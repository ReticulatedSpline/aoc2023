﻿using System.Formats.Tar;
using System.Text.RegularExpressions;

static (List<(int,int)>, List<List<int>>) readFile(string filePath) {
    using (StreamReader sr = new StreamReader(filePath)) {
        string? line;
        List<(int, int)> ruleList = new List<(int, int)>();
        List<List<int>> pageList = new List<List<int>>();

        while ((line = sr.ReadLine()) != null &&
               !string.IsNullOrWhiteSpace(line))
        {
            MatchCollection numbers = Regex.Matches(line, @"\d+");
            ruleList.Add((int.Parse(numbers[0].Value),
                          int.Parse(numbers[1].Value)));
        }

        while ((line = sr.ReadLine()) != null)
        {
            MatchCollection numbers = Regex.Matches(line, @"\d+");
            List<int> pages = new List<int>();
            foreach (Match number in numbers) {
                pages.Add(int.Parse(number.Value));
            }
            pageList.Add(pages);
        }

        return (ruleList, pageList);
    }
}

static bool isValidPageSet(List<(int,int)> rules, List<int> pageSet) {
    foreach (var rule in rules) {
        if (pageSet.IndexOf(rule.Item1) >= 0 && pageSet.IndexOf(rule.Item2) >= 0 &&
            pageSet.IndexOf(rule.Item1) > pageSet.IndexOf(rule.Item2)) {
            return false;
        } 
    }
    return true;
}

int partOneSum = 0;
int partTwoSum = 0;
if (args.Length == 0) {
    Console.WriteLine("Missing input file argument!");
    return;
}

var (rules, pageDir) = readFile(args[0]);
bool isValid;

foreach (var pageSet in pageDir) {
    isValid = true;
    foreach (var page in pageSet) {
        if (!isValidPageSet(rules, pageSet)) {
            isValid = false;
        } 
    }
    if (isValid) {
        partOneSum += pageSet[pageSet.Count / 2];
    }
}

Console.WriteLine($"Part One: {partOneSum}");
Console.WriteLine($"Part Two: {partTwoSum}");