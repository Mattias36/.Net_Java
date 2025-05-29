package org.example;

import java.util.*;

public class Result {
    public Map<Item, Integer> solution = new HashMap<>();
    public int totalWeight = 0;
    public int totalValue = 0;

    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder();
        sb.append("Wybrane przedmioty:\n");
        for (Map.Entry<Item, Integer> entry : solution.entrySet()) {
            sb.append(String.format("  %s – wybrany %d razy\n", entry.getKey(), entry.getValue()));
        }
        sb.append(String.format("Total weight: %d\n", totalWeight));
        sb.append(String.format("Total value: %d\n", totalValue));
        return sb.toString();
    }

}