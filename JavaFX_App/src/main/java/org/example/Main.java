package org.example;

import javafx.application.Application;
import javafx.event.ActionEvent;
import javafx.scene.Scene;
import javafx.scene.Node;
import javafx.event.EventHandler;
import javafx.scene.control.*;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.*;
import javafx.scene.text.Text;
import javafx.stage.Stage;

public class Main extends Application {

    private ImageView originalImageView;
    private Button executeBtn, saveImageBtn, resizeBtn, rotateLeftBtn, rotateRightBtn, resetImageBtn;
    private Image processedImage, originalImage;
    private boolean isImageModified = false;

    @Override
    public void start(Stage primaryStage) {
        primaryStage.setTitle("Aplikacja do przetwarzania obrazów");

        BorderPane layout = new BorderPane();
        layout.setTop(buildTopBar());
        layout.setCenter(buildMainContent(primaryStage));
        layout.setBottom(new Label("Autor: Mateusz Marciak 272599, Politechnika Wrocławska"));

        AppLogger.info("Uruchomiono aplikację");
        primaryStage.setScene(new Scene(layout, 600, 700));
        primaryStage.show();
    }

    private HBox buildTopBar() {
        ImageView logo = new ImageView(new Image("file:logo_pwr.png"));
        logo.setFitHeight(50);
        logo.setPreserveRatio(true);
        Label title = new Label("Przetwarzanie obrazów - JavaFX");
        return new HBox(10, logo, title);
    }

    private VBox buildMainContent(Stage stage) {
        Text welcome = new Text("Witaj w aplikacji do przetwarzania obrazów!");

        ComboBox<String> operations = new ComboBox<>();
        operations.setPromptText("Wybierz operację");
        operations.getItems().addAll("Negatyw", "Progowanie", "Konturowanie");

        executeBtn = new Button("Wykonaj");
        executeBtn.setDisable(true);
        executeBtn.setOnAction(e -> handleOperation(operations.getValue()));

        originalImageView = new ImageView();
        originalImageView.setFitWidth(300);
        originalImageView.setPreserveRatio(true);

        saveImageBtn = createButton("Zapisz obraz", false, e -> showSaveDialog());
        resizeBtn = createButton("Zmień rozmiar", false, e -> showResizeDialog());
        resetImageBtn = createButton("Resetuj obraz", false, e -> resetToOriginal());
        rotateLeftBtn = createButton("\u2190", false, e -> rotateImage(true));
        rotateRightBtn = createButton("\u2192", false, e -> rotateImage(false));
        rotateLeftBtn.setVisible(false);
        rotateRightBtn.setVisible(false);

        Button loadImageBtn = new Button("Wczytaj obraz");
        loadImageBtn.setOnAction(e -> loadImage(stage));

        VBox leftControl = new VBox(rotateLeftBtn);
        VBox rightControl = new VBox(rotateRightBtn);
        leftControl.setAlignment(javafx.geometry.Pos.CENTER);
        rightControl.setAlignment(javafx.geometry.Pos.CENTER);

        HBox imageRow = new HBox(20, leftControl, originalImageView, rightControl);
        imageRow.setAlignment(javafx.geometry.Pos.CENTER);


        HBox operationRow = new HBox(10, operations, executeBtn);
        operationRow.setStyle("-fx-alignment: center;");

        VBox center = new VBox(15, welcome, loadImageBtn, imageRow, operationRow, resizeBtn, resetImageBtn, saveImageBtn);
        center.setStyle("-fx-padding: 20; -fx-alignment: center;");

        return center;
    }

    private Button createButton(String text, boolean enabled, EventHandler<ActionEvent> handler) {
        Button btn = new Button(text);
        btn.setDisable(!enabled);
        btn.setOnAction(handler);
        return btn;
    }

    private void adjustImageViewSize(Image image) {
        if (image.getWidth() >= image.getHeight()) {
            originalImageView.setFitWidth(300);
            originalImageView.setFitHeight(0);
        } else {
            originalImageView.setFitHeight(300);
            originalImageView.setFitWidth(0);
        }
    }

    private void loadImage(Stage stage) {
        Image image = ImageLoader.loadJPG(stage);
        if (image != null) {
            originalImageView.setImage(image);
            adjustImageViewSize(image);
            processedImage = image;
            originalImage = image;
            isImageModified = false;
            resetImageBtn.setDisable(false);
            executeBtn.setDisable(false);
            saveImageBtn.setDisable(false);
            resizeBtn.setDisable(false);
            rotateLeftBtn.setDisable(false);
            rotateRightBtn.setDisable(false);
            rotateLeftBtn.setVisible(true);
            rotateRightBtn.setVisible(true);

            UIUtils.showAlert(Alert.AlertType.INFORMATION, "Pomyślnie załadowano plik");
            AppLogger.info("Wczytano obraz do aplikacji");
        }
    }

    private void resetToOriginal() {
        if (originalImage != null) {
            originalImageView.setImage(originalImage);
            adjustImageViewSize(originalImage);
            processedImage = originalImage;
            isImageModified = false;
            UIUtils.showAlert(Alert.AlertType.INFORMATION, "Obraz przywrócono do stanu początkowego.");
            AppLogger.info("Użytkownik zresetował obraz do oryginału");
        }
    }
    private void handleOperation(String selected) {
        if (selected == null) {
            UIUtils.showAlert(Alert.AlertType.WARNING, "Nie wybrano operacji do wykonania");
            return;
        }
        try {
            switch (selected) {
                case "Negatyw" -> {
                    AppLogger.info("Wykonywanie operacji: Negatyw");
                    applyImage(ImageProcessor.generateNegative(originalImageView.getImage()), "Negatyw został wygenerowany pomyślnie!");
                }
                case "Progowanie" -> {
                    AppLogger.info("Rozpoczęto operację: Progowanie");
                    showThresholdDialog();
                }
                case "Konturowanie" -> {
                    AppLogger.info("Wykonywanie operacji: Konturowanie");
                    applyImage(ImageProcessor.generateEdges(originalImageView.getImage()), "Konturowanie zostało przeprowadzone pomyślnie!");
                }
            }
        } catch (Exception ex) {
            AppLogger.error("Błąd podczas wykonywania operacji: " + ex.getMessage());
            UIUtils.showAlert(Alert.AlertType.ERROR, "Nie udało się wykonać operacji.");
        }
    }

    private void applyImage(Image image, String successMessage) {
        processedImage = image;
        originalImageView.setImage(image);
        adjustImageViewSize(image);
        isImageModified = true;
        if (!successMessage.isEmpty()) {
            AppLogger.info(successMessage);
            UIUtils.showAlert(Alert.AlertType.INFORMATION, successMessage);
        }
    }


    private void rotateImage(boolean left) {
        AppLogger.info("Wykonano obrót obrazu o 90 stopni " + (left ? "w lewo" : "w prawo"));
        Image image = left ? ImageProcessor.rotateLeft(originalImageView.getImage()) : ImageProcessor.rotateRight(originalImageView.getImage());
        applyImage(image, "");
    }

    private void showResizeDialog() {
        Dialog<Void> dialog = new Dialog<>();
        dialog.setTitle("Skalowanie obrazu");

        ButtonType applyButtonType = new ButtonType("Zmień rozmiar", ButtonBar.ButtonData.OK_DONE);
        ButtonType resetButtonType = new ButtonType("Przywróć oryginalne wymiary", ButtonBar.ButtonData.OTHER);
        dialog.getDialogPane().getButtonTypes().addAll(applyButtonType, resetButtonType, ButtonType.CANCEL);

        TextField widthField = new TextField();
        TextField heightField = new TextField();
        widthField.setPromptText("Szerokość (1–3000)");
        heightField.setPromptText("Wysokość (1–3000)");

        Label widthValidation = new Label();
        Label heightValidation = new Label();
        widthValidation.setStyle("-fx-text-fill: red;");
        heightValidation.setStyle("-fx-text-fill: red;");

        VBox content = new VBox(10,
                new Label("Szerokość:"), widthField, widthValidation,
                new Label("Wysokość:"), heightField, heightValidation);
        dialog.getDialogPane().setContent(content);

        Node applyButton = dialog.getDialogPane().lookupButton(applyButtonType);
        applyButton.addEventFilter(ActionEvent.ACTION, event -> {
            widthValidation.setText("");
            heightValidation.setText("");

            String widthText = widthField.getText().trim();
            String heightText = heightField.getText().trim();

            if (widthText.isEmpty()) {
                widthValidation.setText("Pole jest wymagane");
                event.consume();
            }
            if (heightText.isEmpty()) {
                heightValidation.setText("Pole jest wymagane");
                event.consume();
            }

            try {
                int width = Integer.parseInt(widthText);
                int height = Integer.parseInt(heightText);

                if (width <= 0 || width > 3000) {
                    widthValidation.setText("Zakres: 1–3000");
                    event.consume();
                }

                if (height <= 0 || height > 3000) {
                    heightValidation.setText("Zakres: 1–3000");
                    event.consume();
                }

                if (!event.isConsumed()) {
                    processedImage = ImageProcessor.resize(originalImageView.getImage(), width, height);
                    originalImageView.setImage(processedImage);
                    isImageModified = true;
                    AppLogger.info("Zmieniono rozmiar na :" + width + "x" + height);
                }

            } catch (NumberFormatException ex) {
                widthValidation.setText("Nieprawidłowa liczba");
                heightValidation.setText("Nieprawidłowa liczba");
                event.consume();
            }
        });

        Node resetButton = dialog.getDialogPane().lookupButton(resetButtonType);
        resetButton.addEventFilter(ActionEvent.ACTION, event -> {
            if (processedImage != null && originalImage != null) {
                int originalWidth = (int) originalImage.getWidth(); 
                int originalHeight = (int) originalImage.getHeight();

                processedImage = ImageProcessor.resize(processedImage, originalWidth, originalHeight);
                originalImageView.setImage(processedImage);
                adjustImageViewSize(processedImage);

                AppLogger.info("Przywrócono oryginalne wymiary obrazu.");
                UIUtils.showAlert(Alert.AlertType.INFORMATION, "Przywrócono oryginalne wymiary.");
            }
        });




        dialog.showAndWait();
    }

    private void showSaveDialog() {
        Dialog<Void> dialog = new Dialog<>();
        dialog.setTitle("Zapisz obraz");

        ButtonType saveButtonType = new ButtonType("Zapisz", ButtonBar.ButtonData.OK_DONE);
        dialog.getDialogPane().getButtonTypes().addAll(saveButtonType, ButtonType.CANCEL);

        TextField filenameField = new TextField();
        filenameField.setPromptText("Nazwa pliku (3-100 znaków)");

        Label validationLabel = new Label();
        validationLabel.setStyle("-fx-text-fill: red;");

        VBox content = new VBox(10, new Label("Podaj nazwę pliku:"), filenameField, validationLabel);
        dialog.getDialogPane().setContent(content);

        Node saveButton = dialog.getDialogPane().lookupButton(saveButtonType);
        saveButton.addEventFilter(ActionEvent.ACTION, event -> {
            String filename = filenameField.getText().trim();

            if (!isImageModified) {
                validationLabel.setStyle("-fx-text-fill: orange;");
                validationLabel.setText("Na pliku nie zostały wykonane żadne operacje!");
                event.consume();
                return;
            }

            if (filename.length() < 3) {
                validationLabel.setText("Wpisz co najmniej 3 znaki");
                event.consume();
                return;
            }

            if (filename.length() > 100) {
                validationLabel.setText("Nazwa pliku jest za długa");
                event.consume();
                return;
            }

            boolean success = ImageSaver.saveToPicturesFolder(processedImage, filename);
            if (success) {
                AppLogger.info("Zapisano obraz jako: " + filename + ".jpg");
                UIUtils.showAlert(Alert.AlertType.INFORMATION, "Zapisano obraz jako: " + filename + ".jpg");
            } else {
                AppLogger.info("Nie udało się zapisać pliku.");
                UIUtils.showAlert(Alert.AlertType.ERROR, "Nie udało się zapisać pliku.");
            }
        });

        dialog.showAndWait();
    }

    private void showThresholdDialog() {
        Dialog<Void> dialog = new Dialog<>();
        dialog.setTitle("Progowanie");

        ButtonType applyButtonType = new ButtonType("Wykonaj progowanie", ButtonBar.ButtonData.OK_DONE);
        dialog.getDialogPane().getButtonTypes().addAll(applyButtonType, ButtonType.CANCEL);

        TextField thresholdField = new TextField();
        thresholdField.setPromptText("Wartość progu (0–255)");
        Label validationLabel = new Label();
        validationLabel.setStyle("-fx-text-fill: red;");

        VBox content = new VBox(10,
                new Label("Podaj wartość progu:"), thresholdField, validationLabel);
        dialog.getDialogPane().setContent(content);

        Node applyButton = dialog.getDialogPane().lookupButton(applyButtonType);
        applyButton.addEventFilter(ActionEvent.ACTION, event -> {
            String input = thresholdField.getText().trim();
            if (input.isEmpty()) {
                validationLabel.setText("Pole jest wymagane");
                event.consume();
                return;
            }

            try {
                int threshold = Integer.parseInt(input);
                if (threshold < 0 || threshold > 255) {
                    validationLabel.setText("Zakres: 0–255");
                    event.consume();
                    return;
                }

                try {
                    processedImage = ImageProcessor.applyThreshold(originalImageView.getImage(), threshold);
                    originalImageView.setImage(processedImage);
                    isImageModified = true;
                    UIUtils.showAlert(Alert.AlertType.INFORMATION, "Progowanie zostało przeprowadzone pomyślnie!");
                } catch (Exception ex) {
                    UIUtils.showAlert(Alert.AlertType.ERROR, "Nie udało się wykonać progowania.");
                }

            } catch (NumberFormatException ex) {
                validationLabel.setText("Nieprawidłowa liczba");
                event.consume();
            }
        });

        dialog.showAndWait();
    }

    @Override
    public void stop() {
        AppLogger.info("Zamknięto aplikację");
        ParallelProcessor.shutdown();
    }

    public static void main(String[] args) {
        launch(args);
    }
}
