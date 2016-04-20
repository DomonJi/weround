using System.Collections.Generic;
using UnityEngine;


public class RoundItem
{
	/// <summary>
	/// 
	/// </summary>
	public int[][] waves { get; set; }

	/// <summary>
	/// 
	/// </summary>
	public int antiNum { get; set; }

	/// <summary>
	/// 
	/// </summary>
	public int canVInfect { get; set; }
}

public class RoundConfig
{
	/// <summary>
	/// 
	/// </summary>
	public RoundItem[][] levels { get; set; }
}
