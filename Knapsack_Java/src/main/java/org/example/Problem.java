package org.example;

import java.util.*;

public class Problem {
    public List<Item> items;

    public Problem(int n, int seed, int lower, int upper) {
        Random rand = new Random(seed);
        items = new ArrayList<>();
        for (int i = 0; i < n; i++) {
            int value = lower + rand.nextInt(upper - lower + 1);
            int weight = lower + rand.nextInt(upper - lower + 1);
            items.add(new Item(value, weight));
        }
    }

    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder();
        sb.append("Wygenerowane przedmioty:\n");
        for (int i = 0; i < items.size(); i++) {
            sb.append(String.format("  %d. %s\n", i + 1, items.get(i)));
        }
        return sb.toString();
    }


    public Result solve(int capacity) {
        items.sort((a, b) -> Double.compare(b.getRatio(), a.getRatio()));
        Result result = new Result();
        for (Item item : items) {
            int count = capacity / item.weight;
            if (count > 0) {
                result.solution.put(item, count);
                result.totalWeight += item.weight * count;
                result.totalValue += item.value * count;
                capacity -= item.weight * count;
            }
        }
        return result;
    }
}
