package sample;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.layout.Background;
import javafx.scene.layout.BackgroundFill;
import javafx.scene.layout.Pane;
import javafx.scene.paint.Color;
import javafx.scene.shape.Rectangle;
import javafx.stage.Stage;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class Main extends Application {

    private static final Random random = new Random();
    private Pane mainCanvas;

    private Thread rainThread = new Thread(this::runRainThread);
    private final List<RainDrop> rainDropList = new ArrayList<>();

    public void setup(){
        for (int i = 0; i < 150; i++) {
            spawnRainDrop(true);
        }

        rainThread.start();
    }

    public void update()
    {
        Platform.runLater(new Runnable() {
            @Override
            public void run() {

                for (RainDrop rainDrop : rainDropList)
                {
                    Rectangle rectangle = rainDrop.getRectangle();

                    double y = rectangle.getX();
                    double height = mainCanvas.getHeight();

                    if (rectangle.getY() > mainCanvas.getHeight())
                    {
                        rectangle.setX(random.nextDouble() * mainCanvas.getWidth());
                        rectangle.setY(0);
                        continue;
                    }
                    rectangle.setY(rectangle.getY() + rainDrop.getSpeed());
                    rectangle.setY(rectangle.getY() + rainDrop.getSpeed());
                }
            }
        });
    }

    public void runRainThread(){
        while (true)
        {
            try {
                Thread.sleep(1);
                update();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }

    private void spawnRainDrop(boolean freshStart){
        RainDrop rainDrop = new RainDrop();
        Rectangle rectangle = rainDrop.getRectangle();
        double height = mainCanvas.getHeight();
        double width = mainCanvas.getWidth();

        if (freshStart)
        {
            rectangle.setX(random.nextDouble() * mainCanvas.getWidth());
            rectangle.setY(random.nextDouble() * mainCanvas.getHeight());
        }
        else
        {
            rectangle.setX(random.nextDouble() * mainCanvas.getWidth());
            rectangle.setY(0);
        }

        this.rainDropList.add(rainDrop);
        this.mainCanvas.getChildren().add(rectangle);
    }

    @Override
    public void start(Stage primaryStage) throws Exception{
        Parent root = FXMLLoader.load(getClass().getResource("sample.fxml"));
        primaryStage.setTitle("Hello World");
        primaryStage.setScene(new Scene(root));
        primaryStage.show();
        this.mainCanvas = (Pane) root;
        mainCanvas.setBackground(new Background(new BackgroundFill(Color.BLACK, null, null)));
        setup();
    }


    public static void main(String[] args) {
        launch(args);
    }
}
