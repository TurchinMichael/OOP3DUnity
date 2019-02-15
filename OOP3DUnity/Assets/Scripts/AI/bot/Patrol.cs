using UnityEngine;
using UnityEngine.AI;

namespace Geekbrains
{
	public static class Patrol
	{
		public static Vector3 GenericPoint(Transform agent)
		{
			Vector3 result;

			var dis = Random.Range(10, 500);
			var randomPoint = Random.insideUnitSphere * dis;

			NavMesh.SamplePosition(agent.position + randomPoint, out var hit, dis, NavMesh.AllAreas);
			result = hit.position;

			return result;
        }

        public static Vector3 GenericPoint(float yOffset)
        {
            Vector3 result;

            var dis = Random.Range(10, 500);
            var randomPoint = Random.insideUnitSphere * dis;

            NavMesh.SamplePosition(Vector3.zero + randomPoint, out var hit, dis, NavMesh.AllAreas);
            result = new Vector3(hit.position.x, hit.position.y + yOffset, hit.position.z);

            return result;
        }
    }
}