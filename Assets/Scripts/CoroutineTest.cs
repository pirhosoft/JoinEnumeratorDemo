using System.Collections;
using UnityEngine;

namespace PiRhoSoft
{
	public class CoroutineTest : MonoBehaviour
	{
		public void YieldBreakTest()
		{
			Log("Starting YieldBreakCoroutine");
			StartCoroutine(YieldBreakCoroutine());
		}

		public void YieldBreakAlternateTest()
		{
			var yieldBreak = YieldBreakAlternateCoroutine();
			Log("Starting YieldBreakCoroutine");
			StartCoroutine(yieldBreak);
		}

		public void YieldBreakJoinedTest()
		{
			Log("Starting JoinEnumerator");
			StartCoroutine(new JoinEnumerator(YieldBreakCoroutine()));
		}

		public void ComplexTest()
		{
			Log("Starting ComplexCoroutine");
			StartCoroutine(ComplexCoroutine());
		}

		public void ComplexJoinedTest()
		{
			Log("Starting ComplexCoroutine Joined");
			StartCoroutine(new JoinEnumerator(ComplexCoroutine()));
		}

		private IEnumerator YieldBreakCoroutine()
		{
			Log(" - YieldBreakCoroutine Started");
			yield return YieldBreak();
			Log(" - YieldBreakCoroutine Finished");
		}

		private IEnumerator YieldBreakAlternateCoroutine()
		{
			Log(" - YieldBreakCoroutine Started");
			yield return StartCoroutine(YieldBreak());
			Log(" - YieldBreakCoroutine Finished");
		}

		private IEnumerator YieldBreak()
		{
			Log(" - YieldBreak");
			yield break;
		}

		private IEnumerator ComplexCoroutine()
		{
			Log(" - ComplexCoroutine Started");

			Log(" - Yielding null");
			yield return null;

			Log(" - Yielding WaitForSeconds");
			yield return new WaitForSeconds(1.0f);

			Log(" - Yielding IEnumerator");
			yield return ComplexCoroutine1();

			Log(" - ComplexCoroutine Finished");
		}

		private IEnumerator ComplexCoroutine1()
		{
			Log(" -   Depth 1 Started");
			Log(" -   Yielding IEnumerator");
			yield return ComplexCoroutine21();
			Log(" -   Depth 1 Continuing");
			Log(" -   Yielding IEnumerator");
			yield return ComplexCoroutine22();
			Log(" -   Depth 1 Finished");
		}

		private IEnumerator ComplexCoroutine21()
		{
			Log(" -     Depth 2-1 Started");
			Log(" -     Breaking");
			yield break;
		}

		private IEnumerator ComplexCoroutine22()
		{
			Log(" -     Depth 2-2 Started");
			Log(" -     Yielding IEnumerator");
			yield return ComplexCoroutine3();
			Log(" -     Depth 2-2 Finished");
		}

		private IEnumerator ComplexCoroutine3()
		{
			Log(" -       Depth 3 Started");
			Log(" -       Yielding Null");
			yield return null;
			Log(" -       Depth 3 Finished");
		}

		private void Log(string message)
		{
			Debug.LogFormat("Frame {0}: {1}", Time.frameCount, message);
		}
	}
}