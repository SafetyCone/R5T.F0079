using System;


namespace R5T.F0079
{
	public class ComponentOperator : IComponentOperator
	{
		#region Infrastructure

	    public static IComponentOperator Instance { get; } = new ComponentOperator();

	    private ComponentOperator()
	    {
        }

	    #endregion
	}
}