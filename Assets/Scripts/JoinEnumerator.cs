using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PiRhoSoft
{
	public class JoinEnumerator : IEnumerator
	{
		private IEnumerator _root;
		private Stack<IEnumerator> _enumerators = new Stack<IEnumerator>(10);

		public object Current
		{
			get { return _enumerators.Peek().Current; }
		}

		public JoinEnumerator(IEnumerator coroutine)
		{
			_root = coroutine;
			_enumerators.Push(coroutine);
		}

		public bool MoveNext()
		{
			while (_enumerators.Count > 0)
			{
				var enumerator = _enumerators.Peek();
				var next = enumerator.MoveNext();

				// three scenarios
				//  - enumerator has no next: resume the parent (unless this is the root)
				//  - enumerator has a next and it is an IEnumerator: process that enumerator
				//  - enumerator has a next and it is something else: yield

				if (!next)
					_enumerators.Pop();
				else if (enumerator.Current is IEnumerator child && !(child is CustomYieldInstruction))
					_enumerators.Push(child);
				else
					break;
			}

			return _enumerators.Count > 0;
		}

		public void Reset()
		{
			while (_enumerators.Count > 0)
				_enumerators.Pop();

			_enumerators.Push(_root);
			_root.Reset();
		}
	}
}