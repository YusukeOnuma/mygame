using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CSVDownroad : MonoBehaviour {

    // ギミックデータ格納用の配列データ(とりあえず初期値はnull値)
    private int[,] enemyDatas;

    // CSVから切り分けられた文字列型２次元配列データ 
    private string [,] sdataArrays; 

    //読み込めたか確認の表示用の変数
    private int height = 0;    //行数
    private int width = 0;    //列数

    // CSVデータを文字列型２次元配列に変換する
    //                      ファイルパス,変換される配列の値(参照渡し)
	public int[,] readCSVData(string path)
    {
        // ストリームリーダーsrに読み込む
        StreamReader sr     = new StreamReader(path);
        // ストリームリーダーをstringに変換
        string strStream    = sr.ReadToEnd( );
        // StringSplitOptionを設定(要はカンマとカンマに何もなかったら格納しないことにする)
        System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;

        // 行に分ける
        string [ ] lines = strStream.Split(new char [ ] { '\r', '\n' }, option);

        // カンマ分けの準備(区分けする文字を設定する)
        char [ ] spliter = new char [1] { ',' };

        // 行数設定
        int h = lines.Length;
        // 列数設定
        int w = lines[0].Split(spliter, option).Length;

        // 返り値の2次元配列の要素数を設定
		enemyDatas = new int [h, w];

        // 行データを切り分けて,2次元配列へ変換する
        for(int i = 0; i < h; i++)
        {
            string [ ] splitedData = lines [i].Split(spliter, option);

            for(int j = 0; j < w; j++)
            {
				enemyDatas [i, j] = int.Parse(splitedData [j]);
            }
        }

        // 確認表示用の変数(行数、列数)を格納する
        this.height = h;    //行数   
        this.width  = w;    //列数

		return enemyDatas;
    }

	public int getWidth {
		get{return width;}
	}
	public int getHeight {
		get{return height;}
	}


}
