using System.Collections;
using thelab.mvc;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : Controller<SteamPipe87Application>
{

    public override void OnNotification(Message p_event, UnityEngine.Object p_target, params object[] p_data)
    {
        switch (p_event)
        {
            case Message.mainMenuUp:
                app.view.menuView.mainMenu.GetComponent<Animation>().Play("MainMenuUp");
                break;
            case Message.mainMenuDown:
                app.view.menuView.mainMenu.GetComponent<Animation>().Play("MainMenuDown");
                break;
            case Message.mainMenuHalfUp:
                app.view.menuView.mainMenu.GetComponent<Animation>().Play("MainMenuHalfUp");
                break;
            case Message.mainMenuHalfDown:
                app.view.menuView.mainMenu.GetComponent<Animation>().Play("MainMenuHalfDown");
                break;
            case Message.mainMenuUpAfterHalfUp:
                app.view.menuView.mainMenu.GetComponent<Animation>().Play("MainMenuUpAfterHalfUp");
                break;
            case Message.mainMenuDownAfterHalfDown:
                app.view.menuView.mainMenu.GetComponent<Animation>().Play("MainMenuDownAfterHalfUp");
                break;
            case Message.leaderboardMenuUp:
                app.view.menuView.leaderboardMenu.GetComponent<Animation>().Play("Up");
                break;
            case Message.leaderboardMenuDown:
                app.view.menuView.leaderboardMenu.GetComponent<Animation>().Play("Down");
                Notify(Message.clearOldElement, "Leaderboard");
                break;
            case Message.storeMenuUp:
                app.view.menuView.storeMenu.GetComponent<Animation>().Play("Up");
                break;
            case Message.storeMenuDown:
                app.view.menuView.storeMenu.GetComponent<Animation>().Play("Down");
                break;
            case Message.settingsMenuUp:
                app.view.menuView.settingsMenu.GetComponent<Animation>().Play("Up");
                Notify(Message.getPlayerName);
                break;
            case Message.settingsMenuDown:
                app.view.menuView.settingsMenu.GetComponent<Animation>().Play("Down");
                break;
            case Message.aboutMenuUp:
                app.view.menuView.aboutMenu.GetComponent<Animation>().Play("Up");
                break;
            case Message.aboutMenuDown:
                app.view.menuView.aboutMenu.GetComponent<Animation>().Play("Down");
                break;
            case Message.playMenuUp:
                app.view.menuView.playMenu.GetComponent<Animation>().Play("PlayMenuUp");
                break;
            case Message.playMenuDown:
                app.view.menuView.playMenu.GetComponent<Animation>().Play("PlayMenuDown");
                break;
            case Message.playMenuUpAgain:
                app.view.menuView.playMenu.GetComponent<Animation>().Play("PlayMenuUpAgain");
                break;
            case Message.playMenuDownAgain:
                app.view.menuView.playMenu.GetComponent<Animation>().Play("PlayMenuDownAgain");
                break;
            case Message.campaignMenuUp:
                app.view.menuView.campaignMenu.GetComponent<Animation>().Play("Up");
                break;
            case Message.campaignMenuDown:
                app.view.menuView.campaignMenu.GetComponent<Animation>().Play("Down");
                break;
            case Message.campaignMenuUpAgain:
                app.view.menuView.campaignMenu.GetComponent<Animation>().Play("UpAgain");
                break;
            case Message.capmaignMenuDownAgain:
                app.view.menuView.campaignMenu.GetComponent<Animation>().Play("DownAgain");
                break;
            case Message.backButtonRight:
                app.view.backButton.GetComponent<Animation>().Play("BackButtonRight");
                break;
            case Message.backButtonLeft:
                app.view.backButton.GetComponent<Animation>().Play("BackButtonLeft");
                break;
            case Message.clearOldElement:
                switch (p_data[0].ToString())
                {
                    case "Leaderboard":
                        GridLayoutGroup leaderGrid = app.view.menuView.leaderboardMenu.leaderboardscrollContent.GetComponent<GridLayoutGroup>();
                        StartCoroutine(ClearOldElementAfterWait(leaderGrid, 1));
                        break;
                    case "Store":
                        GridLayoutGroup storeGrid = app.view.menuView.storeMenu.storeScrollContent.GetComponent<GridLayoutGroup>();
                        StartCoroutine(ClearOldElementAfterWait(storeGrid, 1));
                        break;
                    default:
                        break;
                }
                break;
            case Message.temporaryDisconnectionBackButtonBoxCollider:
                BoxCollider2D bc = app.view.backButton.GetComponent<BoxCollider2D>();
                bc.enabled = false;
                StartCoroutine(TemporaryDisconnection(bc, 1));
                break;
            case Message.disconnectionCampaignBoxColliders:
                DisconnectionBoxColliders();
                break;
            case Message.disconnectionBackButtonBoxCollider:
                app.view.backButton.GetComponent<BoxCollider2D>().enabled = false;
                break;
            case Message.connectionCampaignBoxColliders:
                StartCoroutine(ConnectionBoxColliders(1));
                break;
            case Message.initializeLeaderboardList:
                InitializeList(p_data[0].ToString());
                break;
            case Message.initializeCampaignList:
                InitializeList(p_data[0].ToString());
                break;
            case Message.initializeStoreList:
                Notify(Message.readAndInitializePlayerBoosts);
                break;
            case Message.returnToDefaultPosition:
                StartCoroutine(ReturnToDefaultPosition());
                break;
            case Message.purchase:
                Purchasing(p_target, p_data[0]);
                break;
            case Message.changeCurrMenu:
                StartCoroutine(ChangeCurrMenu(p_data));
                break;
            default:
                break;
        }
    }

    private IEnumerator ChangeCurrMenu(object[] p_data)
    {
        yield return new WaitForSeconds(1);
        app.model.menuModel.currMenu = (MenuModel.selectMenu)p_data[0];
    }

    private void Purchasing(UnityEngine.Object p_target, object p_data)
    {
        switch (p_target.ToString())
        {
            case "Accelerator (StoreItem)":
                app.model.playerModel.accelerator++;
                app.model.playerModel.goldGear -= new SecureInt(int.Parse(p_data.ToString()));
                break;
            case "Multiplier (StoreItem)":
                app.model.playerModel.multiplier++;
                app.model.playerModel.goldGear -= new SecureInt(int.Parse(p_data.ToString()));
                break;
            case "Distributor (StoreItem)":
                app.model.playerModel.distributor++;
                app.model.playerModel.goldGear -= new SecureInt(int.Parse(p_data.ToString()));
                break;
            case "Cooler (StoreItem)":
                app.model.playerModel.cooler++;
                app.model.playerModel.goldGear -= new SecureInt(int.Parse(p_data.ToString()));
                break;
            case "Moderator (StoreItem)":
                app.model.playerModel.moderator++;
                app.model.playerModel.goldGear -= new SecureInt(int.Parse(p_data.ToString()));
                break;
            default:
                Log(p_target);
                break;
        }
        string playerBoosts = app.model.playerModel.accelerator + "," + app.model.playerModel.multiplier + "," + app.model.playerModel.distributor + "," + app.model.playerModel.cooler + "," + app.model.playerModel.moderator + "," + app.model.playerModel.goldGear;
        SaveLoad.SaveBoosts(playerBoosts);
        Notify(Message.readAndInitializePlayerBoosts);
    }

    private IEnumerator ReturnToDefaultPosition()
    {
        yield return new WaitForSeconds(1);
        app.view.menuView.playMenu.campaignButton.GetComponentInChildren<Scrollbar>().value = 0;
        app.view.menuView.playMenu.campaignButton.GetComponentInChildren<Scrollbar>().enabled = true;
        app.view.menuView.playMenu.raitingButton.GetComponentInChildren<Scrollbar>().value = 0;
        app.view.menuView.playMenu.raitingButton.GetComponentInChildren<Scrollbar>().enabled = true;
    }

    private void ChangeStatusScrollRect()
    {
        app.view.menuView.campaignMenu.GetComponentInChildren<ScrollRect>().enabled = !app.view.menuView.campaignMenu.GetComponentInChildren<ScrollRect>().enabled;
    }

    private IEnumerator ConnectionBoxColliders(int delay)
    {
        yield return new WaitForSeconds(delay);
        BoxCollider2D[] lstBox = app.view.menuView.campaignMenu.campaignScrollContent.GetComponentsInChildren<BoxCollider2D>();
        for (int i = 0; i < lstBox.Length; i++)
        {
            lstBox[i].enabled = true;
        }
    }

    private void DisconnectionBoxColliders()
    {
        BoxCollider2D[] lstBox = app.view.menuView.campaignMenu.campaignScrollContent.GetComponentsInChildren<BoxCollider2D>();
        for (int i = 0; i < lstBox.Length; i++)
        {
            lstBox[i].enabled = false;
        }
    }

    private void InitializeList(string v)
    {
        switch (v)
        {
            case "Leaderboard":
                GridLayoutGroup leaderGrid = app.view.menuView.leaderboardMenu.leaderboardscrollContent.GetComponent<GridLayoutGroup>();

                ClearOldElement(leaderGrid);

                for (int i = 0; i < app.model.menuModel.leaderboardModel.countItems; i++)
                {
                    GameObject newItem = Instantiate(app.view.menuView.leaderboardMenu.leaderboardItem.gameObject) as GameObject;
                    InitializeNewItem(leaderGrid, newItem, i + 1);
                }

                RectTransform leaderRect = app.view.menuView.leaderboardMenu.leaderboardscrollContent.GetComponent<RectTransform>();
                SetContentHeight(leaderGrid, leaderRect);
                break;
            case "Store":
                GridLayoutGroup storeGrid = app.view.menuView.storeMenu.storeScrollContent.GetComponent<GridLayoutGroup>();

                ClearOldElement(storeGrid);

                for (int i = 0; i < app.model.menuModel.storeModel.countItems; i++)
                {
                    GameObject newItem = Instantiate(app.view.menuView.storeMenu.storeItem.gameObject) as GameObject;
                    InitializeNewItem(storeGrid, newItem, i + 1);
                }

                RectTransform storeRect = app.view.menuView.storeMenu.storeScrollContent.GetComponent<RectTransform>();
                SetContentHeight(storeGrid, storeRect);
                break;
            case "Campaign":
                app.model.menuModel.campaignModel.topLevel = PlayerPrefs.GetInt("level", 1);

                GridLayoutGroup campaignGrid = app.view.menuView.campaignMenu.campaignScrollContent.GetComponent<GridLayoutGroup>();

                if (app.model.menuModel.campaignModel.topLevel != campaignGrid.transform.childCount)
                {
                    StartCoroutine(IClearOldElement(campaignGrid));

                    for (int i = 0; i < app.model.menuModel.campaignModel.topLevel; i++)
                    {
                        GameObject newItem = Instantiate(app.view.menuView.campaignMenu.campaignItem.gameObject) as GameObject;
                        InitializeNewItem(campaignGrid, newItem, i + 1);

                        if (PlayerPrefs.HasKey("star" + (i + 1)))
                        {
                            string value = PlayerPrefs.GetString("star" + (i + 1));
                            string[] valueArray = value.Split(',');
                            Image[] stars = newItem.GetComponentsInChildren<Image>();
                            for (int j = 0; j < valueArray.Length; j++)
                            {
                                stars[j].sprite = app.model.menuModel.campaignModel.sprite[0];
                            }
                        }
                    }

                    RectTransform campaignRect = app.view.menuView.campaignMenu.campaignScrollContent.GetComponent<RectTransform>();
                    SetContentHeight(campaignGrid, campaignRect);

                    StartCoroutine(TemporaryDisconnections());
                }
                break;
            default:
                Log(v);
                break;
        }
    }

    private IEnumerator TemporaryDisconnection(BoxCollider2D bc, int time)
    {
        yield return new WaitForSeconds(time);
        bc.enabled = true;
    }

    private IEnumerator TemporaryDisconnections()
    {
        BoxCollider2D[] lstBox = app.view.menuView.campaignMenu.campaignScrollContent.GetComponentsInChildren<BoxCollider2D>();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < lstBox.Length; i++)
        {
            lstBox[i].enabled = true;
        }
    }

    private IEnumerator ClearOldElementAfterWait(GridLayoutGroup gl, int v)
    {
        yield return new WaitForSeconds(1);
        ClearOldElement(gl);
    }

    private void SetContentHeight(GridLayoutGroup glg, RectTransform rt)
    {
        if (glg.name != "CampaignScrollContent")
        {
            float scrollContentHeight = (glg.transform.childCount / glg.constraintCount * glg.cellSize.y) + ((glg.transform.childCount / glg.constraintCount - 1) * glg.spacing.y);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, scrollContentHeight);
        }
        else
        {
            float scrollContentHeight = (((glg.transform.childCount / glg.constraintCount) + 1) * glg.cellSize.y) + ((glg.transform.childCount / glg.constraintCount) * glg.spacing.y);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, scrollContentHeight);
        }
    }

    private void InitializeNewItem(GridLayoutGroup glg, GameObject newItem, int i)
    {
        newItem.name = i.ToString();
        newItem.GetComponentInChildren<Text>().text = i.ToString();
        newItem.transform.parent = glg.transform;
        newItem.transform.localScale = Vector3.one;
        newItem.SetActive(true);
    }

    private void ClearOldElement(GridLayoutGroup glg)
    {
        for (int i = 0; i < glg.transform.childCount; i++)
        {
            Destroy(glg.transform.GetChild(i).gameObject);
        }
    }

    private void ClearOldElementImmediate(GridLayoutGroup glg)
    {
        for (int i = 0; i < glg.transform.childCount; i++)
        {
            DestroyImmediate(glg.transform.GetChild(i).gameObject);
        }
    }

    private IEnumerator IClearOldElement(GridLayoutGroup glg)
    {
        for (int i = 0; i < glg.transform.childCount; i++)
        {
            Destroy(glg.transform.GetChild(i).gameObject);
        }
        yield return new WaitForEndOfFrame();
    }
}
