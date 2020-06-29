using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingWindow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CraftingRecipeUI craftingRecipePrefab;
    [SerializeField] RectTransform recipeUIparent;
    [SerializeField] List<CraftingRecipeUI> craftingRecipeUIs;

    [Header("Public Variables")]
    public Itemcontainer itemcontainer;
    public List<CraftingRecipe> craftingRecipes;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;

    private void OnValidate()
    {
        Init();
    }

    private void Start()
    {
        Init();

        foreach(CraftingRecipeUI craftingRecipeUI in craftingRecipeUIs)
        {
            craftingRecipeUI.OnPointerEnterEvent += OnPointerEnterEvent;
            craftingRecipeUI.OnPointerExitEvent += OnPointerExitEvent;
        }
    }

    private void Init()
    {
        recipeUIparent.GetComponentsInChildren<CraftingRecipeUI>(includeInactive: true, result: craftingRecipeUIs);
        UpdateCraftingRepices();
    }

    private void UpdateCraftingRepices()
    {
        for(int i = 0; i < craftingRecipes.Count; i++)
        {
            if(craftingRecipeUIs.Count == i)
            {
                craftingRecipeUIs.Add(Instantiate(craftingRecipePrefab, recipeUIparent, false));
            }
            else if (craftingRecipeUIs[i] == null)
            {
                craftingRecipeUIs[i] = Instantiate(craftingRecipePrefab, recipeUIparent, false);
            }

            craftingRecipeUIs[i].itemcontainer = itemcontainer;
            craftingRecipeUIs[i].CraftingRecipe = craftingRecipes[i];
        }

        for(int i = craftingRecipes.Count; i < craftingRecipeUIs.Count ; i++)
        {
            craftingRecipeUIs[i].CraftingRecipe = null;
        }
    }
}
