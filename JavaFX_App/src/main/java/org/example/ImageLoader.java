package org.example;

import javafx.scene.control.Alert;
import javafx.scene.image.Image;
import javafx.stage.FileChooser;
import javafx.stage.Window;
import java.io.File;

public class ImageLoader {

    public static Image loadJPG(Window parentWindow) {
        FileChooser fileChooser = new FileChooser();
        fileChooser.setTitle("Wybierz plik JPG");
        fileChooser.getExtensionFilters().add(
                new FileChooser.ExtensionFilter("Pliki JPG", "*.jpg")
        );

        File selectedFile = fileChooser.showOpenDialog(parentWindow);
        if (selectedFile != null && selectedFile.getName().toLowerCase().endsWith(".jpg")) {
            return new Image(selectedFile.toURI().toString());
        } else if (selectedFile != null) {
            UIUtils.showAlert(Alert.AlertType.ERROR, "Niedozwolony format pliku");
        }
        return null;
    }
}
