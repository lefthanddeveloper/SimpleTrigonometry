using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigonometry : MonoBehaviour
{
	public float amplitude = 1f;
	public float zAmplitude = 1f;
	// Update is called once per frame
	float timer;
	float zMoveTimer;
	public float rotationSpeed = 1f;
	public float zMoveSpeed = 10f;


	public Transform indicatorTr;
	[SerializeField] LineRenderer circleLineRenderer;
	[SerializeField] LineRenderer radiusLineRenderer;
	[SerializeField] LineRenderer angleLineRenderer;

	private bool enterNegative;
	private bool isCycledOnce;

	[Header("UI")]
	[SerializeField] private Text text_angle;
	[SerializeField] private Text text_xy;
	private void Start()
	{
		circleLineRenderer.positionCount = 1;
		circleLineRenderer.SetPosition(0, new Vector3(Mathf.Cos(0) * amplitude, Mathf.Sin(0) * amplitude, 0));

		radiusLineRenderer.positionCount = 2;
		radiusLineRenderer.SetPosition(0, Vector3.zero);
		radiusLineRenderer.SetPosition(1, circleLineRenderer.GetPosition(circleLineRenderer.positionCount - 1));

		angleLineRenderer.positionCount = 1;
		Vector3 circleLineRendererInitPos = circleLineRenderer.GetPosition(0);
		angleLineRenderer.SetPosition(0, circleLineRendererInitPos * 0.2f);
	}
	void Update()
	{
		timer += Time.deltaTime * rotationSpeed;
		zMoveTimer += Time.deltaTime * zMoveSpeed;
		float y = Mathf.Sin(timer) * amplitude;
		float x = Mathf.Cos(timer) * amplitude;
		float z = Mathf.Sin(zMoveTimer) * zAmplitude;

		PositionIndicator(new Vector3(x, y, z));

		DrawCircleLine();
		DrawRadiusLine();
		DrawAngleCircleLine();

		CheckCycle(y, x);

	}

	void PositionIndicator(Vector3 position)
	{
		indicatorTr.position = position;
	}

	void DrawCircleLine()
	{
		circleLineRenderer.positionCount++;
		circleLineRenderer.SetPosition(circleLineRenderer.positionCount - 1, indicatorTr.position);
	}

	void DrawAngleCircleLine()
	{
		angleLineRenderer.positionCount++;
		Vector3 circleLineCurPos = circleLineRenderer.GetPosition(circleLineRenderer.positionCount - 1);
		Vector3 destPos = circleLineCurPos * 0.2f;
		angleLineRenderer.SetPosition(angleLineRenderer.positionCount - 1, destPos);
	}

	void DrawRadiusLine()
	{
		radiusLineRenderer.SetPosition(1, circleLineRenderer.GetPosition(circleLineRenderer.positionCount - 1));
	}

	void CheckCycle(float y, float x)
	{
		float angle = Mathf.Atan2(y, x);
		float angleInDeg = angle * Mathf.Rad2Deg;

		if (angle <= 0)
		{
			enterNegative = true;
		}

		if (enterNegative && angle >= 0)
		{
			enterNegative = false;
			ResetCircle();
		}

		//UI
		text_angle.text = string.Format("θ: {0}° ", angleInDeg.ToString("F0"));
		text_xy.text = string.Format("({0} , {1})", x.ToString("F1"), y.ToString("F1"));

		void ResetCircle()
		{
			circleLineRenderer.positionCount = 0;
			angleLineRenderer.positionCount = 0;
		}
	}
}
