package org.example;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class ProblemTest {
    @Test
    public void testItemGenerationCount() {
        Problem p = new Problem(10, 42, 1, 10);
        assertEquals(10, p.items.size());
    }

    @Test
    public void testValueRange() {
        Problem p = new Problem(10, 42, 1, 10);
        for (Item i : p.items) {
            assertTrue(i.value >= 1 && i.value <= 10);
            assertTrue(i.weight >= 1 && i.weight <= 10);
        }
    }

    @Test
    public void testSolutionNotEmpty() {
        Problem p = new Problem(5, 42, 1, 10);
        Result r = p.solve(20);
        assertFalse(r.solution.isEmpty());
    }

    @Test
    public void testSolutionEmptyForZeroCapacity() {
        Problem p = new Problem(5, 42, 1, 10);
        Result r = p.solve(0);
        assertEquals(0, r.totalWeight);
        assertEquals(0, r.totalValue);
    }
}