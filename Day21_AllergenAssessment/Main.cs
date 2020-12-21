using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdventOfCode.Day21
{
    public class Main
    {
        public static string PartOne()
        {
            var lines = File.ReadAllLines(@"Day21_AllergenAssessment\input.txt");

            var allergensDict = new Dictionary<string, List<string>>();
            var ingredientsDict = new Dictionary<string, long>();

            foreach (var line in lines)
            {
                var ingredients = line
                    .Split(" (contains ")[0]
                    .Split(" ")
                    .ToList();

                var allergens = line
                    .Split(" (contains ")[1].TrimEnd(')')
                    .Split(", ")
                    .ToList();

                foreach (var allergen in allergens)
                    if (allergensDict.TryGetValue(allergen, out var allergenIngredients))
                        allergensDict[allergen] = ingredients.Intersect(allergenIngredients).ToList();
                    else
                        allergensDict[allergen] = ingredients;

                foreach (var ingredient in ingredients)
                    if (ingredientsDict.TryGetValue(ingredient, out var ingredientCount))
                        ingredientsDict[ingredient] = ingredientCount + 1;
                    else
                        ingredientsDict[ingredient] = 1;
            }

            while (allergensDict.Values.Any(x => x.Count > 1))
                foreach (var exact in allergensDict.Where(x => x.Value.Count == 1))
                    foreach (var allergen in allergensDict)
                        if (allergen.Key != exact.Key)
                            allergensDict[allergen.Key] = allergen.Value.Except(exact.Value).ToList();

            foreach (var allergen in allergensDict)
                ingredientsDict.Remove(allergen.Value.Single());

            return ingredientsDict.Select(x => x.Value).Aggregate((a, b) => a + b).ToString();
        }

        public static string PartTwo()
        {
            var lines = File.ReadAllLines(@"Day21_AllergenAssessment\input.txt");

            var allergensDict = new Dictionary<string, List<string>>();
            var ingredientsDict = new Dictionary<string, long>();

            foreach (var line in lines)
            {
                var ingredients = line
                    .Split(" (contains ")[0]
                    .Split(" ")
                    .ToList();

                var allergens = line
                    .Split(" (contains ")[1].TrimEnd(')')
                    .Split(", ")
                    .ToList();

                foreach (var allergen in allergens)
                    if (allergensDict.TryGetValue(allergen, out var allergenIngredients))
                        allergensDict[allergen] = ingredients.Intersect(allergenIngredients).ToList();
                    else
                        allergensDict[allergen] = ingredients;

                foreach (var ingredient in ingredients)
                    if (ingredientsDict.TryGetValue(ingredient, out var ingredientCount))
                        ingredientsDict[ingredient] = ingredientCount + 1;
                    else
                        ingredientsDict[ingredient] = 1;
            }

            while (allergensDict.Values.Any(x => x.Count > 1))
                foreach (var exact in allergensDict.Where(x => x.Value.Count == 1))
                    foreach (var allergen in allergensDict)
                        if (allergen.Key != exact.Key)
                            allergensDict[allergen.Key] = allergen.Value.Except(exact.Value).ToList();

            return string.Join(",", allergensDict
                .OrderBy(x => x.Key)
                .Select(x=>x.Value.Single()));
        }

    }
}
