package org.example;

import javafx.embed.swing.SwingFXUtils;
import javafx.scene.image.Image;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.File;

public class ImageSaver {

    public static boolean saveToPicturesFolder(Image image, String filename) {
        try {
            String userPictures = System.getProperty("user.home") + File.separator + "Pictures";
            File outputFile = new File(userPictures, filename + ".jpg");

            if (outputFile.exists()) return false;

            BufferedImage bImage = SwingFXUtils.fromFXImage(image, null);
            BufferedImage rgbImage = new BufferedImage(bImage.getWidth(), bImage.getHeight(), BufferedImage.TYPE_INT_RGB);
            rgbImage.getGraphics().drawImage(bImage, 0, 0, null);

            ImageIO.write(rgbImage, "jpg", outputFile);
            return true;
        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
    }
}

