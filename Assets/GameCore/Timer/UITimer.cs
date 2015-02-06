using UnityEngine;
using System.Collections;

public enum UITimerEvtType
{
		INTERVAL,
		TIMEUP,
}

public delegate void UITimerDelegate (UITimerEvtType type,float leftTime);

public class UITimer : IUpdateListener
{
		float _leftTime;

		public float leftTime {
				get {
						return _leftTime;
				}
		}

		float _intervalTime;
		bool _isPause;
		bool _addListener;
		float _curIntervalTime;

		public event UITimerDelegate timeEvt;

		void Invoke (UITimerEvtType type)
		{
				if (timeEvt != null)
						timeEvt.Invoke (type, _leftTime);
		}

		public bool isPause {
				get {
						return _isPause;
				}
		}

		public void StartTimer (float lastTime, float intervalTime)
		{
				_leftTime = lastTime;
				_intervalTime = intervalTime;
				_curIntervalTime = lastTime;
				_isPause = false;
				if (!_addListener)
						GameCore.GetInstance ().AddUpdaterListener (this);
		}

		public void Pause ()
		{
				if (_isPause)
						return;
				_isPause = true;
		}

		public void Resume ()
		{
				if (!_isPause)
						return;
				_isPause = false;
		}

		public void StopTimer ()
		{
				if (_addListener)
						GameCore.GetInstance ().RemoveUpdaterListener (this);
		}

		void Timeup ()
		{
				Invoke (UITimerEvtType.TIMEUP);
		}

		public void FrameUpdate (float deltaTime)
		{
				if (!_isPause) {
						float oldTime = _leftTime;
						_leftTime -= deltaTime;
						if (_leftTime <= 0) {
								Timeup ();
						} else {
								if (Mathf.Floor (_curIntervalTime * 100) == Mathf.Floor (_intervalTime * 100) + Mathf.Floor (_leftTime * 100)) {
										_curIntervalTime = _leftTime;
										Invoke (UITimerEvtType.INTERVAL);
								}
						}
				}
		}

}
