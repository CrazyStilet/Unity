using thelab.mvc;
using UnityEngine;

public class FieldView : View<SteamPipe87Application> {

    private void Update()
    {
        if (!app.model.gameModel.pause)
        {
            FieldItem[] field = gameObject.GetComponentsInChildren<FieldItem>();

            if (field[0].transform.position.y <= -5.2f)
            {
                for (int i = 0; i < app.model.gameModel.fieldModel.columns; i++)
                {
                    float offset = app.model.gameModel.fieldModel.rows * 105;
                    Destroy(field[i]);
                    GameObject Image = Instantiate(app.model.gameModel.fieldModel.pipePrefubs[0],
                        new Vector3(
                            app.view.gameView.fieldMenu.fieldView.transform.position.x,
                            app.view.gameView.fieldMenu.fieldView.transform.position.y + offset,
                            app.view.gameView.fieldMenu.fieldView.transform.position.z),
                        Quaternion.identity);

                    Image.transform.SetParent(app.view.gameView.fieldMenu.fieldView.transform);
                    Notify(Message.regroup);
                }

                FieldItem[] items = app.view.gameView.fieldMenu.fieldView.GetComponentsInChildren<FieldItem>();

                for (int i = 0; i < app.model.gameModel.fieldModel.rows; i++)
                {
                    for (int j = 0; j < app.model.gameModel.fieldModel.columns; j++)
                    {
                        app.model.gameModel.fieldModel.fieldPipes[i, j] = items[i * app.model.gameModel.fieldModel.columns + j];
                    }
                }

                Notify(Message.changeSteps);
            }
        }
    }
}
