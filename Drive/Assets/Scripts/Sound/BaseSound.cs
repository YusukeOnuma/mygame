using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSound : Singleton<BaseSound>
{

    /// SEチャンネル数
    const int SE_CHANNEL = 4;

    /// サウンド種別
    enum eType
    {
        Bgm, // BGM
        Se,  // SE
    }

    // サウンド再生のためのゲームオブジェクト
    private GameObject __object = null;
    // サウンドリソース
    private AudioSource __sourceBgm = null; // BGM
    private AudioSource __sourceSeDefault = null; // SE (デフォルト)
    private AudioSource[] __sourceSeArray; // SE (チャンネル)
                                           // BGMにアクセスするためのテーブル
    private Dictionary<string, __List> __poolBgm = new Dictionary<string, __List>();
    // SEにアクセスするためのテーブル 
    private Dictionary<string, __List> __poolSe = new Dictionary<string, __List>();

    /// 保持するデータ
    class __List
    {
        /// アクセス用のキー
        public string Key;
        /// リソース名
        public string ResName;
        /// AudioClip
        public AudioClip Clip;

        /// コンストラクタ
        public __List(string key, string res)
        {
            Key = key;
            ResName = "Sounds/" + res;
            // AudioClipの取得
            Clip = Resources.Load(ResName) as AudioClip;
        }
    }

    /// コンストラクタ
    public BaseSound()
    {
        // チャンネル確保
        __sourceSeArray = new AudioSource[SE_CHANNEL];
    }

    /// AudioSourceを取得する
    AudioSource __GetAudioSource(eType type, int channel = -1)
    {
        if (__object == null)
        {
            // GameObjectがなければ作る
            __object = new GameObject("Sound");
            // 破棄しないようにする
            GameObject.DontDestroyOnLoad(__object);
            // AudioSourceを作成
            __sourceBgm = __object.AddComponent<AudioSource>();
            __sourceSeDefault = __object.AddComponent<AudioSource>();
            for (int i = 0; i < SE_CHANNEL; i++)
            {
                __sourceSeArray[i] = __object.AddComponent<AudioSource>();
            }
        }

        if (type == eType.Bgm)
        {
            // BGM
            return __sourceBgm;
        }
        else
        {
            // SE
            if (0 <= channel && channel < SE_CHANNEL)
            {
                // チャンネル指定
                return __sourceSeArray[channel];
            }
            else
            {
                // デフォルト
                return __sourceSeDefault;
            }
        }
    }

    // サウンドのロード
    // ※Resources/Soundsフォルダに配置すること
    public static void LoadBgm(string key, string resName)
    {
        Instance.__LoadBgm(key, resName);
    }
    public static void LoadSe(string key, string resName)
    {
        Instance.__LoadSe(key, resName);
    }
    void __LoadBgm(string key, string resName)
    {
        if (__poolBgm.ContainsKey(key))
        {
            __poolBgm.Remove(key);
        }
        __poolBgm.Add(key, new __List(key, resName));
    }
    void __LoadSe(string key, string resName)
    {
        if (__poolSe.ContainsKey(key))
        {
            // すでに登録済みなのでいったん消す
            __poolSe.Remove(key);
        }
        __poolSe.Add(key, new __List(key, resName));
    }

    /// BGMの再生
    /// ※事前にLoadBgmでロードしておくこと
    public static bool PlayBgm(string key)
    {
        return Instance.__PlayBgm(key);
    }
    bool __PlayBgm(string key)
    {
        if (__poolBgm.ContainsKey(key) == false)
        {
            // 対応するキーがない
            return false;
        }

        // いったん止める
        __StopBgm();

        // リソースの取得
        var data = __poolBgm[key];

        //Debug.Log(data.Clip);

        // 再生
        //var source = __GetAudioSource(eType.Bgm);
        //source.loop = true;
        //source.clip = data.Clip;
        //source.Play();

        return true;
    }
    /// BGMの停止
    public static bool StopBgm()
    {
        return Instance.__StopBgm();
    }
    bool __StopBgm()
    {
        __GetAudioSource(eType.Bgm).Stop();

        return true;
    }

    public static void SetValBgm(float num)
    {
        Instance.__ValBgm(num);
    }

    private void __ValBgm(float num)
    {
        var source = __GetAudioSource(eType.Bgm);
        source.volume = num;
    }

    /// SEの再生
    /// ※事前にLoadSeでロードしておくこと
    public static bool PlaySe(string key, int channel = -1)
    {
        return Instance.__PlaySe(key, channel);
    }
    bool __PlaySe(string key, int channel = -1)
    {
        if (__poolSe.ContainsKey(key) == false)
        {
            // 対応するキーがない
            return false;
        }

        // リソースの取得
        var _data = __poolSe[key];

        //if (0 <= channel && channel < SE_CHANNEL)
        //{
        //    // チャンネル指定
        //    var source = __GetAudioSource(eType.Se, channel);
        //    source.clip = _data.Clip;
        //    source.Play();
        //}
        //else
        //{
        //    // デフォルトで再生
        //    var source = __GetAudioSource(eType.Se);
        //    source.PlayOneShot(_data.Clip);
        //}

        return true;
    }

    public static bool StopSe(string key, int channel = -1)
    {
        return Instance.__StopSe(key, channel);
    }

    bool __StopSe(string key, int channel = -1)
    {
        if (__poolSe.ContainsKey(key) == false)
        {
            // 対応するキーがない
            return false;
        }

        // リソースの取得
        var _data = __poolSe[key];

        if (0 <= channel && channel < SE_CHANNEL)
        {
            // チャンネル指定
            var source = __GetAudioSource(eType.Se, channel);
            source.clip = _data.Clip;
            source.Stop();
        }
        else
        {
            // デフォルトで再生
            //var source = __GetAudioSource(eType.Se);
            //source.PlayOneShot(_data.Clip);
        }

        return true;
    }

    public static void SetValSe(float num)
    {
        Instance.__ValSe(num);
    }

    private void __ValSe(float num)
    {
        for (int i = 0; i < SE_CHANNEL; i++)
        {
            var source = __GetAudioSource(eType.Se, i);
            source.volume = num;
        }
    }

}
