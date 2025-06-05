package org.example;

import javafx.scene.image.Image;
import javafx.scene.image.PixelReader;
import javafx.scene.image.PixelWriter;
import javafx.scene.image.WritableImage;
import javafx.scene.paint.Color;

import java.util.concurrent.ExecutionException;

public class ImageProcessor {

    public static Image generateNegative(Image inputImage) throws InterruptedException, ExecutionException {
        return ParallelProcessor.process(inputImage, (reader, writer, width, startY, endY) -> {
            for (int y = startY; y < endY; y++) {
                for (int x = 0; x < width; x++) {
                    Color color = reader.getColor(x, y);
                    Color negColor = new Color(
                            1.0 - color.getRed(),
                            1.0 - color.getGreen(),
                            1.0 - color.getBlue(),
                            1.0
                    );
                    writer.setColor(x, y, negColor);
                }
            }
        });
    }

    public static Image applyThreshold(Image inputImage, int threshold) throws InterruptedException, ExecutionException {
        return ParallelProcessor.process(inputImage, (reader, writer, width, startY, endY) -> {
            for (int y = startY; y < endY; y++) {
                for (int x = 0; x < width; x++) {
                    Color color = reader.getColor(x, y);
                    double gray = (color.getRed() + color.getGreen() + color.getBlue()) / 3;
                    Color binaryColor = (gray * 255 < threshold) ? Color.BLACK : Color.WHITE;
                    writer.setColor(x, y, binaryColor);
                }
            }
        });
    }

    public static Image generateEdges(Image inputImage) throws InterruptedException, ExecutionException {
        return ParallelProcessor.process(inputImage, (reader, writer, width, startY, endY) -> {
            for (int y = Math.max(1, startY); y < Math.min(inputImage.getHeight() - 1, endY); y++) {
                for (int x = 1; x < width - 1; x++) {
                    Color center = reader.getColor(x, y);
                    Color right = reader.getColor(x + 1, y);
                    Color bottom = reader.getColor(x, y + 1);

                    double dx = Math.abs(center.getBrightness() - right.getBrightness());
                    double dy = Math.abs(center.getBrightness() - bottom.getBrightness());

                    double edge = Math.min(1.0, dx + dy);
                    writer.setColor(x, y, new Color(edge, edge, edge, 1.0));
                }
            }
        });
    }

    public static Image resize(Image inputImage, int width, int height) {
        if (inputImage == null || width <= 0 || height <= 0) return null;

        WritableImage outputImage = new WritableImage(width, height);
        PixelReader reader = inputImage.getPixelReader();
        PixelWriter writer = outputImage.getPixelWriter();

        double scaleX = inputImage.getWidth() / width;
        double scaleY = inputImage.getHeight() / height;

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                int srcX = (int) (x * scaleX);
                int srcY = (int) (y * scaleY);
                writer.setColor(x, y, reader.getColor(srcX, srcY));
            }
        }

        return outputImage;
    }

    public static Image rotateLeft(Image inputImage) {
        int width = (int) inputImage.getWidth();
        int height = (int) inputImage.getHeight();

        WritableImage outputImage = new WritableImage(height, width);
        var reader = inputImage.getPixelReader();
        var writer = outputImage.getPixelWriter();

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                writer.setColor(y, width - 1 - x, reader.getColor(x, y));
            }
        }
        return outputImage;
    }

    public static Image rotateRight(Image inputImage) {
        int width = (int) inputImage.getWidth();
        int height = (int) inputImage.getHeight();

        WritableImage outputImage = new WritableImage(height, width);
        var reader = inputImage.getPixelReader();
        var writer = outputImage.getPixelWriter();

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                writer.setColor(height - 1 - y, x, reader.getColor(x, y));
            }
        }
        return outputImage;
    }
}
