using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 設定動畫結束時的callback
public delegate void AnimateCallBack(); 
 
// 界面動畫管理
public class InterfaceAnimator { 
    public InterfaceController _interface;

    private AnimateWorkList _animateList;

    public InterfaceAnimator(InterfaceController inter) {
        _interface = inter;
        _animateList = new AnimateWorkList();
    }

    public void update() {
        _animateList.update();
    }

    public void prepareShiftIn() {
        _animateList.pushWorker(
            new AnimateWorker(
                new AnimateMoveTo(_interface._charPlayerPlace._character.GetComponent<RectTransform>(), Vector2.zero, 80),
                new AnimateMoveTo(_interface._charEnemyPlace._character.GetComponent<RectTransform>(), Vector2.zero, 80) )
        ).pushWorker( 
            new AnimateWorker(
                new AnimateMoveTo(_interface._charPlayerPlace._bottomLine.GetComponent<RectTransform>(), Vector2.zero, 30),
                new AnimateMoveTo(_interface._charPlayerPlace._posture.GetComponent<RectTransform>(), Vector2.zero, 30),
                new AnimateMoveTo(_interface._charEnemyPlace._bottomLine.GetComponent<RectTransform>(), Vector2.zero, 30),
                new AnimateMoveTo(_interface._charEnemyPlace._posture.GetComponent<RectTransform>(), Vector2.zero, 30) )
        ).pushWorker( 
            new AnimateWorker(
                new AnimateMoveTo(_interface.getImageTopBar().GetComponent<RectTransform>(), Vector2.zero, 30),
                new AnimateMoveTo(_interface.getImageBottomBar().GetComponent<RectTransform>(), Vector2.zero, 30) )
        ).pushWorker(
            new AnimateWorker(
                new AnimateMoveTo(_interface.getImageMainMenu().GetComponent<RectTransform>(), Vector2.zero, 50) )
        ).pushWorker(
            new AnimateWorker(
                new AnimateMoveTo(_interface.getImageDiceBox().GetComponent<RectTransform>(), new Vector2(-100f, 0f), 30)
            ).addWorks(
                _interface._teamPlayerStatus.getShiftInAnimates()
            ).addWorks(
                _interface._teamEnemyStatus.getShiftInAnimates() )
        ).pushWorker(
            new AnimateWorker(
                _interface._teamPlayerStatus.getActiveShiftAnimate(),
                _interface._teamEnemyStatus.getActiveShiftAnimate() )
        ).pushWorker(
            new AnimateWorker(
                new AnimateFadeIn(_interface._attrPoints._pointTable.transform, 50),
                new AnimateFadeIn(_interface._towerStatus._towerTable.transform, 50),
                new AnimateFadeIn(_interface._attrPointsEnemy._pointTable.transform, 50),
                new AnimateFadeIn(_interface._towerStatusEnemy._towerTable.transform, 50)
            ).setEnd(() => {
                // 起始動畫完成，開始第一回合
                _interface._battle.nextTurn(); }
            ) ); 
    }
    public void StartMovTurn() {    
        _animateList.addNewWorker( 
            new AnimateWorker(
                new AnimateMoveTo(_interface.getImageMovTurn().GetComponent<RectTransform>(), Vector2.zero, 30),
                new AnimateMoveTo(_interface.getImageTurnInfo().GetComponent<RectTransform>(), Vector2.zero, 30),
                new AnimateFadeIn(_interface.getImageMovTurn().transform, 30),
                new AnimateFadeIn(_interface.getImageTurnInfo().transform, 30 )
            ).setStart(() => {
                // 準備顯示移動階段資訊
                _interface.showMoveTurn(); }
            ).setEnd(() => { 
                // 顯示擲骰按鈕
                _interface.showThrowButton(); }
            ) );
    }
    public void CollectFaceDecision() {
        _animateList.addNewWorker(
            new AnimateWorker(
                new AnimateFadeIn(_interface.getImageAttrDecisionBack().GetComponent<RectTransform>(), 20 ),
                new AnimateFadeIn(_interface.getImageBaseDecisionBack().GetComponent<RectTransform>(), 20 )
            ).setStart( () => { 
                _interface.changeAttrDecision(); }
            ).setEnd(() => {
                // 選擇欄位建立後 將3D骰子移除
                _interface.removeDices3D();
                // 顯示移動階段動作選擇按鈕
                _interface.showMoveActionButton(); }
            ) );
    }
    public void AggregateAttrTower() { 
        AnimateWorkThread workThread = _animateList.addNewWorker(
            new AnimateWorker( _interface._attrDecision.getAttrAggregateAnimate() ) 
        );
        workThread.pushWorker(
            _interface._attrDecision.getAttrBackFadeAnimate()
        );

        if (_interface._towerStatus.isBuildTower()) {
            workThread.pushWorker(
                new AnimateWorker( _interface._attrDecision.getBaseAggregateAnimate() )
            );
        }
        AnimateWorker build = new AnimateWorker();
        if (_interface._towerStatus.isBuildTower()) {
            build.addWorks( _interface._towerStatus.getBuildTowerAnimateWorkers() );
        }
        if (_interface._towerStatusEnemy.isBuildTower()) {
            build.addWorks(_interface._towerStatusEnemy.getBuildTowerAnimateWorkers());
        }
        workThread.pushWorker(build);
        workThread.pushWorker(
            _interface._attrDecision.getBaseBackFadeAnimate()
        );

        workThread.pushWorker(
            new AnimateWorker().setEnd( () => { 
                _interface.hideMoveActionButton();
                _interface.hideNextButton(); }
            ) );
    }
    public void StartPlayerAtkTurn() {         
        _animateList.addNewWorker( new AnimateWorker(
                new AnimateMoveTo(_interface.getImagePlayerAtkTurn().GetComponent<RectTransform>(), Vector2.zero, 30),
                new AnimateMoveTo(_interface.getImageEnemyDefTurn().GetComponent<RectTransform>(), Vector2.zero, 30),
                new AnimateFadeIn(_interface.getImageAtkTurn().transform, 30 )
            ).setStart(() => {
                // 準備顯示移動階段資訊
                _interface.showPlayerAtkTurn(); }
            ) );
    }
    public void StartPlayerDefTurn() {
        _animateList.addNewWorker( new AnimateWorker(
                new AnimateMoveTo(_interface.getImagePlayerDefTurn().GetComponent<RectTransform>(), Vector2.zero,30),
                new AnimateMoveTo(_interface.getImageEnemyAtkTurn().GetComponent<RectTransform>(), Vector2.zero, 30),
                new AnimateFadeIn(_interface.getImageDefTurn().transform, 30 )
            ).setStart(() => {
                // 準備顯示移動階段資訊
                _interface.showPlayerDefTurn(); }
            ) );
    }

    public void checkDiceBox(int type) {
        _interface._diceBox.checkDiceBox(type);
        _animateList.addNewWorker( 
            new AnimateWorker( _interface._diceBox.getModeAnimates() )
        ).pushWorker( 
            new AnimateWorker( _interface._diceBox.getDiceStackShowAnimate() ) );
    }
}

// 動畫順序流程管理
public class AnimateWorkList {
    private List<AnimateWorkThread> _list;
    public AnimateWorkList() { _list = new List<AnimateWorkThread>(); }

    public AnimateWorkThread pushWorker(AnimateWorker work) {
        if (_list.Count > 0) { _list[0].pushWorker(work); }
        else {
            AnimateWorkThread workThread = new AnimateWorkThread();
            workThread.pushWorker(work);
            _list.Add(workThread);
        }
        return _list[0];
    }
    public AnimateWorkThread addNewWorker(AnimateWorker work){
        AnimateWorkThread workThread = new AnimateWorkThread();
        workThread.pushWorker(work);
        _list.Add(workThread);
        return _list[0];
    }
    public AnimateWorkThread addNewWorkList(params AnimateWorker[] workers) {
        AnimateWorkThread workThread = new AnimateWorkThread();
        workThread.pushWorkerList(workers);
        _list.Add(workThread);
        return _list[0];
    }

    public void update() {
        for (int i = 0; i < _list.Count; i++) {
            _list[i].update();
            if (_list[i].isEnd()) { _list.Remove(_list[i]); }
        }  
    }
}

public class AnimateWorkThread {
    private Queue<AnimateWorker> _workThread;
    public AnimateWorkThread() { _workThread = new Queue<AnimateWorker>(); }
    public AnimateWorkThread pushWorker(AnimateWorker worker) {
        _workThread.Enqueue(worker);
        return this;
    }
    public AnimateWorkThread pushWorkerList(params AnimateWorker[] workerList) {
        for(int i = 0; i < workerList.Length; i++) {
            _workThread.Enqueue( workerList[i] );
        }
        return this;
    }
    public void update() {
        AnimateWorker currentWork = _workThread.Peek();
        currentWork.update();
        if (currentWork.isEnd()) { _workThread.Dequeue(); }
    }
    public bool isEnd() { return _workThread.Count == 0; }
}

public class AnimateWorker {
    private enum WorkMode { ready = 1, busy, end };
    private WorkMode _mode = WorkMode.ready;
    private AnimateWork[] _works;
    private AnimateCallBack _startFunc;
    private AnimateCallBack _endFunc;
    private float _waitBefore = 0;

    public AnimateWorker(params AnimateWork[] works) { _works = works; }
    public AnimateWorker setStart(AnimateCallBack call) { _startFunc = call; return this; }
    public AnimateWorker setEnd(AnimateCallBack call) { _endFunc = call; return this; }
    public AnimateWorker setWaitBefore(float wait) { _waitBefore = wait; return this; }
    public AnimateWorker addWorks(params AnimateWork[] works) {
        AnimateWork[] newWorks = new AnimateWork[_works.Length + works.Length];
        for (int i = 0; i < _works.Length; i++) { newWorks[i] = _works[i]; }
        for (int i = 0; i < works.Length; i++) { newWorks[_works.Length+i] = works[i]; }
        _works = newWorks;
        return this;
    }
    public AnimateWorker mergeWorker(AnimateWorker worker) {
        AnimateWorker merge = new AnimateWorker(_works);
        merge.addWorks(worker._works);
        merge.setStart(() => {
            if (_startFunc != null) _startFunc();
            if (worker._startFunc != null) worker._startFunc();
        });
        merge.setEnd(() => {
            if (_endFunc != null) _endFunc();
            if (worker._endFunc != null) worker._endFunc();
        });
        merge.setWaitBefore(Mathf.Max( _waitBefore, worker._waitBefore));
        return merge;
    }
    public bool isEnd() { return _mode == WorkMode.end; }

    public void update() {
        if (_mode == WorkMode.ready) {
            if (_waitBefore > 0) { 
                _waitBefore -= 1; 
            } else {
                _mode = WorkMode.busy;
                if (_startFunc != null) { _startFunc(); }
            }
        } else if (_mode == WorkMode.busy) {
            bool isEnd = true;
            for (int i = 0; i < _works.Length; i++) {
                _works[i].update();
                if ( !_works[i].isEnd() ) {
                    isEnd = false;
                }else if ( _works[i].isEnd() && _works[i].hasNextWork() ) {
                    isEnd = false;
                    _works[i] = _works[i].getNextWork();
                }
            }
            if (isEnd) { 
                _mode = WorkMode.end;
                if (_endFunc != null) { _endFunc(); }
            }
        }
    }
}

public class AnimateWork {
    protected enum WorkMode { ready = 1, busy, end };
    protected WorkMode _mode = WorkMode.ready;
    protected AnimateWork _nextWork;
    protected AnimateWork _combineWork;
    protected AnimateCallBack _startFunc;
    protected AnimateCallBack _endFunc;
    protected float _time = 0;
    protected float _current = 0;
    protected float _waitBefore = 0;

    public bool isEnd() { return _mode == WorkMode.end; }
    public bool hasNextWork() { return _nextWork != null; }

    public AnimateWork setStart(AnimateCallBack call) { _startFunc = call; return this; }
    public AnimateWork setEnd(AnimateCallBack call) { _endFunc = call; return this; }
    public AnimateWork setWaitBefore(float wait) { _waitBefore = wait; return this; }
    public AnimateWork setNextWork(AnimateWork next) { _nextWork = next; return _nextWork; }
    public AnimateWork getNextWork() { return _nextWork; }
    public AnimateWork setCombineWork(AnimateWork combine) { _combineWork = combine; return _combineWork; }
    public AnimateWork getCombineWork() { return _combineWork; }
    
    public virtual void update() {
        if (_combineWork != null) { _combineWork.update(); }
        if (_mode == WorkMode.ready) {
            if (_waitBefore > 0) { 
                _waitBefore -= 1; 
            } else {
                _mode = WorkMode.busy;
                if (_startFunc != null) { _startFunc(); }
                start();
            }
        } else if (_mode == WorkMode.busy) {
            if (_current < _time) {
                _current += 1;
                work();
            } else {
                _mode = WorkMode.end;
                end();
                if (_endFunc != null) { _endFunc(); } 
            } 
        }
    }
    public virtual void start() {}
    public virtual void work() { _mode = WorkMode.end; }
    public virtual void end() {}

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
    public AnimateMoveFromTo( RectTransform rect, Vector2 start, Vector2 target, float time) {
        _rect = rect;
        _start = start;
        _target = target;
        _time = time;
        _current = 0;
    }
    public override void start() {
        _rect.anchoredPosition = _start;
    }
    public override void work() { 
        _rect.anchoredPosition = Vector2.Lerp(_start, _target, _current / _time); 
    }
}
public class AnimateMoveTo : AnimateWork {
    private RectTransform _rect;
    private Vector2 _start;
    private Vector2 _target;
    public AnimateMoveTo( RectTransform rect, Vector2 target, float time) {
        _rect = rect;
        _target = target;
        _time = time;
        _current = 0;
    }
    public override void start() {
        _start = _rect.anchoredPosition;
    }
    public override void work() {
        _rect.anchoredPosition = Vector2.Lerp(_start, _target, _current / _time);
    } 
}

public class AnimateFadeIn : AnimateWork {
    private Transform _obj;
    public AnimateFadeIn(Transform obj, float time) {
        _obj = obj;
        _time = time;
        _current = 0;
    } 
    public override void work() {
        setAlpha(_obj, _current / _time);
    }  
}

public class AnimateOrderFadeIn : AnimateWork {
    private List<GameObject> _items;
    private float _timeUnit; 
    public AnimateOrderFadeIn(List<GameObject> items, float timeUnit) {
        _items = items;
        _timeUnit = timeUnit;
        _time = _timeUnit * _items.Count - 1;
        _current = 0;
    }
    public override void start() {
        for (int i = 0; i < _items.Count; i++) {
            setAlpha(_items[i].transform, 0f);
        }
    }
    public override void work() {
        int order = Mathf.FloorToInt(_current / _timeUnit);
        float f = (_current - order * _timeUnit) / _timeUnit;
        setAlpha(_items[order].transform, f);
    }
    public override void end() {
        for (int i = 0; i < _items.Count; i++) {
            setAlpha(_items[i].transform, 1f);
        }
    } 
}

public class AnimateFadeOut : AnimateWork {
    private Transform _obj; 
    public AnimateFadeOut(Transform obj, float time) {
        _obj = obj;
        _time = time;
        _current = 0;
    }
    public override void work() {
        setAlpha(_obj, 1f - (_current / _time) );
    }  
}

public class AnimateScaleTo : AnimateWork {
    private Transform _obj;
    private Vector3 _scaleFrom;
    private Vector3 _scaleTo;
    public AnimateScaleTo(Transform obj, Vector3 scale, float time) {
        _obj = obj;
        _scaleTo = scale;
        _time = time;
        _current = 0;
    }
    public override void start() {
        _scaleFrom = _obj.localScale;
    }
    public override void work() {
        _obj.localScale = Vector3.Lerp(_scaleFrom, _scaleTo, (_current / _time));
    }
}
public class AnimateRectTo : AnimateWork {
    private RectTransform _obj;
    private Vector2 _sizeDeltaFrom;
    private Vector2 _sizeDeltaTo;
    public AnimateRectTo(RectTransform obj, Rect rect, float time) {
        _obj = obj;
        _sizeDeltaTo = new Vector2( rect.width, rect.height );
        _time = time;
        _current = 0;
    }
    public AnimateRectTo(RectTransform obj, Vector3 scale, float time) {
        _obj = obj;
        _sizeDeltaTo = new Vector2( obj.rect.width * scale.x, obj.rect.height*scale.y );
        _time = time;
        _current = 0;
    }
    public override void start() {
        _sizeDeltaFrom = _obj.sizeDelta;
    }
    public override void work() {
        _obj.sizeDelta = Vector2.Lerp(_sizeDeltaFrom, _sizeDeltaTo, (_current / _time));
    }
}

public class AnimateCurveMoveToPosition : AnimateWork {
    private Transform _obj;
    private Vector3[] _points; 
    public AnimateCurveMoveToPosition(Transform obj, Vector3[] points, float time) {
        _obj = obj;
        _points = points;
        _time = time;
        _current = 0;
    }
    public override void work() {
        _obj.position = getCurvePoint(_current / _time);
    } 

    public Vector3 getCurvePoint(float progress) {
        List<Vector3> countPoints= new List<Vector3>( _points );
        while( countPoints.Count > 1 ){
            List<Vector3> nextCountPoints = new List<Vector3>();
            for (int i = 0; i < countPoints.Count -1; i++) {
                Vector3 nextPoint = Vector3.Lerp(countPoints[i], countPoints[i + 1], progress);
                nextCountPoints.Add(nextPoint);
            }
            countPoints = nextCountPoints;
        }
        return countPoints[0];
    }
}