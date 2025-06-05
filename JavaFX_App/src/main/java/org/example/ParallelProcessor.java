package org.example;

import javafx.scene.image.Image;
import javafx.scene.image.PixelReader;
import javafx.scene.image.PixelWriter;
import javafx.scene.image.WritableImage;

import java.util.concurrent.*;

public class ParallelProcessor {

    private static final int THREAD_COUNT = 4;
    private static final ExecutorService executor = Executors.newFixedThreadPool(THREAD_COUNT);

    public static Image process(Image input, PixelOperation operation) throws InterruptedException, ExecutionException {
        int width = (int) input.getWidth();
        int height = (int) input.getHeight();
        WritableImage output = new WritableImage(width, height);
        PixelReader reader = input.getPixelReader();
        PixelWriter writer = output.getPixelWriter();

        int chunkHeight = height / THREAD_COUNT;
        Future<?>[] tasks = new Future[THREAD_COUNT];

        // zrównoleglenie
        for (int i = 0; i < THREAD_COUNT; i++) {
            final int startY = i * chunkHeight;
            final int endY = (i == THREAD_COUNT - 1) ? height : (i + 1) * chunkHeight;
            tasks[i] = executor.submit(() -> operation.apply(reader, writer, width, startY, endY));
        }

        // zakończenie działania
        for (Future<?> task : tasks) task.get();

        return output;
    }

    public interface PixelOperation {
        void apply(PixelReader reader, PixelWriter writer, int width, int startY, int endY);
    }

    public static void shutdown() {
        executor.shutdown();
    }
}
