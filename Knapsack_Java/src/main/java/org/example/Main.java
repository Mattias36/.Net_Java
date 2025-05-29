package org.example;

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        System.out.print("Podaj liczbę przedmiotów: ");
        int n = scanner.nextInt();
        System.out.print("Podaj ziarno losowania (seed): ");
        int seed = scanner.nextInt();
        System.out.print("Podaj pojemność plecaka: ");
        int capacity = scanner.nextInt();

        Problem p = new Problem(n, seed, 1, 10);
        System.out.println(p);
        Result r = p.solve(capacity);
        System.out.println(r);
    }
}