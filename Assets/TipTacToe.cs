using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipTacToe : MonoBehaviour {
    public GUIStyle titleStyle;
    public GUIStyle chessStyle;

    private int baseLeft = 0;
    private int baseTop = 0;
    
    void OnGUI() {
        // 大框
        GUI.Box(new Rect(baseLeft, baseTop, 350, 520), "");
        // 标题
        GUI.Box(new Rect(baseLeft + 25, baseTop + 30, 300, 60), "");
        GUI.Box(new Rect(baseLeft + 25, baseTop + 30, 300, 60), "Tic Tac Toe", titleStyle);
        // 游戏状态栏
        Rect stateBarPos = new Rect(baseLeft + 25, baseTop + 100, 300, 30);
        // 棋盘
        int blur = GUI.SelectionGrid(new Rect(baseLeft + 25, baseTop + 150, 300, 300), 10, Grid.chess, 3, chessStyle);
        // 重新开始
        if (GUI.Button(new Rect(baseLeft + 25, baseTop + 460, 300, 30), "Restart")) {
            Grid.restart();
        }
        if(0 <= blur && blur < 9 
            && Grid.state != Grid.State.O_win && Grid.state != Grid.State.X_win) {
            Grid.putChess(blur);
        }
        switch (Grid.state) {
            case Grid.State.O_win:
                GUI.Box(stateBarPos, "O win");
                break;
            case Grid.State.X_win:
                GUI.Box(stateBarPos, "X win");
                break;
            case Grid.State.O:
                GUI.Box(stateBarPos, "Turn To O");
                break;
            case Grid.State.X:
                GUI.Box(stateBarPos, "Turn To X");
                break;
            case Grid.State.drawn:
                GUI.Box(stateBarPos, "Drawn");
                break;
            default:
                break;
        }
    }
    private class Grid {
        public static string[] chess = {
        "", "", "",
        "", "", "",
        "", "", "" };
        public static bool OX = true; // true是轮到O方，false是X方
        public enum State{
            O, X, O_win, X_win, drawn
        }; // 游戏状态标识
        public static State state = State.O;

        //重新开始游戏
        public static void restart() {
            OX = true;
            for(int i=0; i!=9; ++i) {
                chess[i] = "";
            }
            state = State.O;
        }
        // 会修改state变量，表示游戏状态
        public static void putChess(int blur) {
            if (chess[blur].CompareTo("") == 0) {
                if(OX) {
                    chess[blur] = "O";
                } else {
                    chess[blur] = "X";
                }
                OX = !OX;
            }
            state = getState();
        }
        private static State getState() {
            // 平局
            bool full = true;
            foreach (string str in chess) {
                if (str.CompareTo("") == 0) {
                    full = false;
                }
            }
            if (full) {
                return State.drawn;
            }
            // 一方胜利

            // 成列
            if (chess[0].CompareTo("") != 0 && chess[0] == chess[3] && chess[3] == chess[6]) {
                return chess[0].CompareTo("O") == 0 ? State.O_win : State.X_win;
            }
            if (chess[1].CompareTo("") != 0 && chess[1] == chess[4] && chess[4] == chess[7]) {
                return chess[1].CompareTo("O") == 0 ? State.O_win : State.X_win;
            }
            if (chess[2].CompareTo("") != 0 && chess[2] == chess[5] && chess[5] == chess[8]) {
                return chess[2].CompareTo("O") == 0 ? State.O_win : State.X_win;
            }

            // 成行
            if (chess[0].CompareTo("") != 0 && chess[0] == chess[1] && chess[1] == chess[2]) {
                return chess[0].CompareTo("O") == 0 ? State.O_win : State.X_win;
            }
            if (chess[3].CompareTo("") != 0 && chess[3] == chess[4] && chess[4] == chess[5]) {
                return chess[3].CompareTo("O") == 0 ? State.O_win : State.X_win;
            }
            if (chess[6].CompareTo("") != 0 && chess[6] == chess[7] && chess[7] == chess[8]) {
                return chess[6].CompareTo("O") == 0 ? State.O_win : State.X_win;
            }

            // 斜线
            if (chess[0].CompareTo("") != 0 && chess[0] == chess[4] && chess[4] == chess[8]) {
                return chess[0].CompareTo("O") == 0 ? State.O_win : State.X_win;
            }
            if (chess[2].CompareTo("") != 0 && chess[2] == chess[4] && chess[4] == chess[6]) {
                return chess[2].CompareTo("O") == 0 ? State.O_win : State.X_win;
            }
            return OX ? State.O : State.X;
        }
    }
}
