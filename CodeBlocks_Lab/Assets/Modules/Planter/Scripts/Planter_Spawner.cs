using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Planter_Spawner : MonoBehaviour
{
	// Components

	public Planter planter;
	public Planter_JSONeer planter_JSONeer;

	//public Transform holder;

	private void Update ()
	{
		if (planter.load)
		{
			planter.load = false;

			// Make a copy of the JSON Container's Categories

			List<Category> JSONCats = planter_JSONeer.Pull_Container_From_JSON ().categories;

			for (int c = 0; c < JSONCats.Count; c++)
			{
				/// For each Category:
				/// Create a Holding Transform, and put it in the game object that will hold everything.

				GameObject currCat = new GameObject (JSONCats[c].name);
				currCat.transform.SetParent (planter.holder);

				// For each Plant:

				for (int p = 0; p < JSONCats[c].plants.Count; p++)
				{
					string currPlantName = JSONCats[c].plants[p].name;

					// Look inside the relevant Planter Category.

					PlanterCategory[] planterCats = planter.categories;

					for (int i = 0; i < planterCats.Length; i++)
					{
						if (JSONCats[c].name == planterCats[i].name)
						{
							// Find the Type that matches the name of the Plant.
							// Spawn that Type's Item at the position supplied by the Plant.
							// Parent the new Item to the Holding Transform we made earlier.

							Type[] types = planterCats[i].types;

							for (int k = 0; k < types.Length; k++)
							{
								if (types[k].item.name == currPlantName)
								{
									Instantiate (types[k].item,
										JSONCats[c].plants[p].position,
										Quaternion.identity,
										currCat.transform);
								}
							}
						}
					}
				}
			}
		}
	}
}
