using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct DataContainer
{
	public List<DataBranch> branchName_STRICT;
}

[System.Serializable]
public struct DataBranch
{
	public string variableName_STRICT;
}