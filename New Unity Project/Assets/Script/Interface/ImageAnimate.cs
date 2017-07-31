using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 設定動畫結束時的callback
public delegate void AnimateCallBack(); 
 
// 界面動畫管理
public class InterfaceAnimator { 
    public BattleController _battle;
    public InterfaceController _interface;

    private AnimateWorkList _animateList;

    public InterfaceAnimator(BattleController battle, InterfaceController inter) {
        _battle = battle;
        _interface = inter;

        _animateList = new AnimateWorkList();
    }

    public void update() {
        _animateList.update();
    }

    public void prepareShiftIn() {
        _animateList.pushWorker(new AnimateWorker(
                new AnimateMoveTo(_interface._charPlayerPlace._character.GetComponent<RectTransform>(), Vector2.zero, 80),
                new AnimateMoveTo(_interface._charEnemyPlace._character.GetComponent<RectTransform>(), Vector2.zero, 80)
            ));
        _animateList.pushWorker(new AnimateWorker(
                new AnimateMoveTo(_interface._charPlayerPlace._bottomLine.GetComponent<RectTransform>(), Vector2.zero, 30),
                new AnimateMoveTo(_interface._charPlayerPlace._posture.GetComponent<RectTransform>(), Vector2.zero, 30),
                new AnimateMoveTo(_interface._charEnemyPlace._bottomLine.GetComponent<RectTransform>(), Vector2.zero, 30),
                new AnimateMoveTo(_interface._charEnemyPlace._posture.GetComponent<RectTransform>(), Vector2.zero, 30)
            ) );
        _animateList.pushWorker(new AnimateWorker(
                new AnimateMoveTo(_interface.getImageTopBar().GetComponent<RectTransform>(), Vector2.zero, 30),
                new AnimateMoveTo(_interface.getImageBottomBar().GetComponent<RectTransform>(), Vector2.zero, 30)
            ) );
        _animateList.pushWorker(new AnimateWorker(
                new AnimateMoveTo(_interface.getImageMainMenu().GetComponent<RectTransform>(), Vector2.zero, 50)
            ) );
        _animateList.pushWorker(new AnimateWorker(
                new AnimateMoveTo(_interface.getImageDiceBox().GetComponent<RectTransform>(), new Vector2(-100f, 0f), 30)
            ).addWorks(_interface._teamPlayerStatus.getShiftInAnimates()
            ).addWorks(_interface._teamEnemyStatus.getShiftInAnimates()
            ) );
        _animateList.pushWorker(new AnimateWorker(
                _interface._teamPlayerStatus.getActiveShiftAnimate(),
                _interface._teamEnemyStatus.getActiveShiftAnimate()
            ) );
        _animateList.pushWorker(new AnimateWorker(
                new AnimateFadeIn(_interface._attrPoints._pointTable.transform, 50),
                new AnimateFadeIn(_interface._towerStatus._towerTable.transform, 50),
                new AnimateFadeIn(_interface._attrPointsEnemy._pointTable.transform, 50),
                new AnimateFadeIn(_interface._towerStatusEnemy._towerTable.transform, 50)
            ) );
        _animateList.pushWorker(new AnimateWorker(
                new AnimateMoveTo(_interface.getImageTurnStatus().GetComponent<RectTransform>(), Vector2.zero, 30),
                new AnimateFadeIn(_interface.getImageTurnInfo().transform, 30 ),
                new AnimateFadeIn(_interface.getImageMovTurn().transform, 30)
            ).setStart(() => {
                _interface.showMoveTurn(); } 
            ).setEnd(() => {
                _battle.nextTurn(); }
            ) );

        // 顯示為移動階段
        //_interface.showMoveTurn();

    }

    public void checkDiceBox(int type) {
        _interface._diceBox.checkDiceBox(type);
        AnimateWork[] animates = _interface._diceBox.getModeAnimates();
        _animateList.addNewWorker(new AnimateWorker(animates));
    }
}

// 動畫順序流程管理
public class AnimateWorkList {
    private List<Queue<AnimateWorker>> _list;
    public AnimateWorkList() { _list = new List<Queue<AnimateWorker>>(); }

    public Queue<AnimateWorker> pushWorker(AnimateWorker work) {
        if (_list.Count > 0) { _list[0].Enqueue(work); }
        else {
            Queue<AnimateWorker> queue = new Queue<AnimateWorker>();
            queue.Enqueue(work);
            _list.Add(queue);
        }
        return _list[0];
    }
    public Queue<AnimateWorker> addNewWorker(AnimateWorker work){
        Queue<AnimateWorker> queue = new Queue<AnimateWorker>();
        queue.Enqueue(work);
        _list.Add(queue);
        return _list[_list.Count - 1];
    }

    public void update() {
        for (int i = 0; i < _list.Count; i++) {
            AnimateWorker currentWork = _list[i].Peek();
            currentWork.update();
            if (currentWork.isEnd()) { _list[i].Dequeue(); }
        }
        _list.RemoveAll( i => i.Count == 0 ); 
    }
}

public class AnimateWorker {
    protected enum WorkMode { ready = 1, busy, end };
    protected WorkMode _mode = WorkMode.ready;     
    private AnimateWork[] _works;
    private AnimateCallBack _startFunc;
    private AnimateCallBack _endFunc;

    public AnimateWorker(params AnimateWork[] works) { _works = works; }
    public AnimateWorker setStart(AnimateCallBack call) { _startFunc = call; return this; }
    public AnimateWorker setEnd(AnimateCallBack call) { _endFunc = call; return this; }
    public AnimateWorker addWorks(params AnimateWork[] works) {
        AnimateWork[] newWorks = new AnimateWork[_works.Length + works.Length];
        for (int i = 0; i < _works.Length; i++) { newWorks[i] = _works[i]; }
        for (int i = 0; i < works.Length; i++) { newWorks[_works.Length+i] = works[i]; }
        _works = newWorks;
        return this;
    }
    public AnimateWorker createWorks(AnimateCallBack call) {
        return this;
    }

    public void update() {
        if (_mode == WorkMode.ready) {
            _mode = WorkMode.busy;
            if (_startFunc != null) { _startFunc(); }
        } else if (_mode == WorkMode.busy) {
            bool isEnd = true;
            for (int i = 0; i < _works.Length; i++) {
                _works[i].work();
                if (!_works[i].isEnd()) isEnd = false;
            }
            if (isEnd) {
                _mode = WorkMode.end;
                if (_endFunc != null) { _endFunc(); }
            }
        }
    }
    public bool isEnd() { return _mode == WorkMode.end; }
}

public class AnimateWork {
    protected enum WorkMode { ready = 1, busy, end };
    protected WorkMode _mode = WorkMode.ready;     
    public virtual void work() { }
    public bool isEnd() { return _mode == WorkMode.end; }

    // 常用函數: 設定透明度
    public static void setAlpha(Transform parent, float alpha ) {
        for(int i=0; i < parent.childCount; i++) {
            setAlpha(parent.GetChild(i), alpha);
        }
        Image img = parent.GetComponent<Image>();
        if (img != null) {
            Color c = img.color;
            c.a = alpha;
            img.color = c;
        }
        Text txt = parent.GetComponent<Text>();
        if(txt != null) {
            Color c = txt.color;
            c.a = alpha;
            txt.color = c;
        }
    }
}

public class AnimateMoveFromTo : AnimateWork {
    private RectTransform _rect;
    private Vector2 _start;
    private Vector2 _target;
    private float _time;
    private float _current;

    public AnimateMoveFromTo( RectTransform rect, Vector2 start, Vector2 target, float time) {
        _rect = rect;
        _start = start;
        _target = target;
        _time = time;
        _current = 0;
    }
    public override void work() {
        if (_mode == WorkMode.ready) {
            _rect.anchoredPosition = _start;
            _mode = WorkMode.busy;
        } else if (_mode == WorkMode.busy) {
            if (_current < _time) {
                _current += 1;
                _rect.anchoredPosition = Vector2.Lerp(_start, _target, _current / _time);
            } else {
                _mode = WorkMode.end;
            }
        }
    }
}
public class AnimateMoveTo : AnimateWork {
    private RectTransform _rect;
    private Vector2 _start;
    private Vector2 _target;
    private float _time;
    private float _current;

    public AnimateMoveTo( RectTransform rect, Vector2 target, float time) {
        _rect = rect;
        _target = target;
        _time = time;
        _current = 0;
    }
    public override void work() {
        if (_mode == WorkMode.ready) {
            _start = _rect.anchoredPosition;
            _mode = WorkMode.busy;
        } else if (_mode == WorkMode.busy) {
            if (_current < _time) {
                _current += 1;
                _rect.anchoredPosition = Vector2.Lerp(_start, _target, _current / _time);
            } else {
                _mode = WorkMode.end;
            }
        }
    }
}
public class AnimateFadeIn : AnimateWork {
    private Transform _obj;
    private float _time;
    private float _current;

    public AnimateFadeIn(Transform obj, float time) {
        _obj = obj;
        _time = time;
        _current = 0;
    }
            
    public override void work(){
        if (_mode == WorkMode.ready) {
            _mode = WorkMode.busy;
        } else if (_mode == WorkMode.busy) {
            if (_current < _time) {
                _current += 1;
                setAlpha(_obj, _current / _time);
            } else {
                _mode = WorkMode.end;
            }
        }
    }
}