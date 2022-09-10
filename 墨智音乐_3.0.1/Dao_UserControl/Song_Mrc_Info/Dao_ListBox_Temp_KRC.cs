﻿using System;
using System.Collections;
using System.IO;
using System.Text;

namespace 墨智音乐_3._0._1.Dao_UserControl.Song_Mrc_Info
{
    class Dao_ListBox_Temp_KRC
    {
        public StreamReader Song_Lrc_StreamReader;//将当前的歌词文件转化临时文件流
        public String A_String_Read;//传递临时生成的歌词时间


        /// <summary>
        /// LRC_Text决定歌词列表空白行数
        /// </summary>
        public int LRC_Text_Null_Nums = 7;

        /// <summary>
        /// 使用歌词行同步
        /// </summary>
        #region 歌词提取（旧版）（半弃用）

        public int Time_1_Middle;	//int 	("," 的位置)
        public int Time_1_End;		//int 	("]" 的位置)
        public int Time_2_Start;	//int 	("<" 的位置)
        public int Time_2_End;      //int 	(">" 的位置)
        public int Start_Time;      //int	开始的时间
        public int Nums_Time;       //int	持续的时间

        public int[] LRC_Nums_Times;  //int数组     存储单个歌词持续的时间
        public string[] LRC_Text_Times;

        public string[] LRC_Time;
        public string[] LRC_Text;

        public static string LRC_Text_Temp;//要返回的歌词Text  

        public double Start_First_Time_1 = 0;
        public double End_Last_Time_1 = 0;
        #region 读取存储歌词文件信息
        /// <summary>
        /// 读取歌词文件中的逐字信息处理并保存在数组中
        /// </summary>
        /// <param name="KRC_One_Text">传入文件流读取的一整行KRC</param>
        /// <returns>返回一句完全由字符串拼接的string整个字符串</returns>
        public string Show_Song_KRC_Text(string KRC_One_Text)
        {
            //创建中间值存储
            string Temp_1 = KRC_One_Text;
            string Temp_2 = KRC_One_Text;
            string Temp_3 = KRC_One_Text;
            string Temp_4;

            LRC_Text_Temp = "";//要返回的歌词Text

            try
            {
                Time_1_Middle = Temp_1.IndexOf(",");//[81751,2131]  下标从0开始
                //MessageBox.Show(KRC_One_Text + "        , 的位置        " + Time_1_Middle);

                Time_1_End = Temp_1.IndexOf("]");
                //MessageBox.Show(KRC_One_Text + "        ] 的位置   " + Time_1_End);

                Start_Time = Convert.ToInt32(Temp_1.Substring(1, Time_1_Middle - 1));
                //MessageBox.Show(KRC_One_Text + "        开始的时间：    " + Start_Time);

                Nums_Time = Convert.ToInt32(Temp_2.Substring(Time_1_Middle + 1, Time_1_End - Time_1_Middle - 1));
                //MessageBox.Show(KRC_One_Text + "        持续的时间：    " + Nums_Time);

                //存储这一行歌词开始与持续的时间
                LRC_Start_And_Middle_Time(Start_Time, Nums_Time);

                Temp_3 = Temp_3.Substring(Time_1_End + 1, Temp_3.Length - Time_1_End - 1);//<0,255,0>胡<255,204,0>歌 <459,253,0>- <712,253,0>六<965,153,0>月<1118,203,0>的<1321,201,0>雨
                Temp_4 = Temp_3;

                LRC_Nums_Times = new int[30];
                LRC_Text_Times = new string[30];

                for (int i = 0; i < LRC_Nums_Times.Length; i++)
                {
                    Temp_3 = Temp_4;

                    if (LRC_Nums_Times[i] == 0 && LRC_Text_Times[i] == null)
                    {
                        /*try
                        {*/
                        //LRC_Nums_Times[i]  保存每个歌词的开始时间
                        string tempss = Temp_3;
                        int middle_nums_start_1 = tempss.IndexOf("<") + 1;//从0开始
                        int middle_nums_start_2 = tempss.IndexOf(",");//从0开始
                        int nums_1 = Convert.ToInt32(tempss.Substring(middle_nums_start_1, middle_nums_start_2 - middle_nums_start_1));//从0开始，选3个-> 255,

                        int middle_nums_1 = Temp_3.IndexOf(",");//<0,255,0>胡<255,204,0>歌 <459,253,0>- <712,253,0>六<965,153,0>月<1118,203,0>的<1321,201,0>雨
                        Temp_3 = Temp_3.Substring(middle_nums_1 + 1, Temp_3.Length - middle_nums_1 - 1);//255,0>胡<255,204,0>歌 <459,253,0>- <712,253,0>六<965,153,0>月<1118,203,0>的<1321,201,0>雨

                        //LRC_Text_Times   保存每个歌词的内容
                        string temp = Temp_3;//保存读取的单个歌词
                        Time_2_End = temp.IndexOf(">") + 1;
                        Time_2_Start = temp.IndexOf("<");
                        if (Time_2_Start > 0)
                        {
                            temp = temp.Substring(Time_2_End, Time_2_Start - Time_2_End);
                        }
                        else
                        {
                            temp = temp.Substring(Time_2_End, temp.Length - Time_2_End);
                        }
                        LRC_Text_Times[i] = temp;


                        string temps = Temp_3;//保存下一次substring的变量
                        Time_2_Start = temps.IndexOf("<");
                        if (Time_2_Start > 0)
                            Temp_4 = temps.Substring(Time_2_Start, temps.Length - Time_2_Start);//<255,204,0>歌 <459,253,0>- <712,253,0>六<965,153,0>月<1118,203,0>的<1321,201,0>雨


                        //LRC_Nums_Times[i]  保存每个歌词的持续时间
                        int middle_nums_2 = Temp_3.IndexOf(",");//从0开始
                        Temp_3 = Temp_3.Substring(0, middle_nums_2);//从0开始，选3个-> 255,
                        LRC_Nums_Times[i] = Convert.ToInt32(Temp_3);
                        int nums_2 = Convert.ToInt32(Temp_3);


                        //nums_1 + Start_Time    总时间制
                        KRC_Start_And_Middle_And_Text(nums_1, nums_2, temp, nums_1 + Start_Time);

                        if (Time_2_Start < 0)
                            break;

                        /*                        }
                                                catch
                                                {
                                                    break;
                                                }*/

                    }
                }

                for (int i = 0; i < LRC_Text_Times.Length; i++)//字符串(char)集合
                {
                    if (LRC_Text_Times[i] != null)
                    {
                        LRC_Text_Temp += LRC_Text_Times[i];
                        if (i + 1 >= LRC_Text_Times.Length || LRC_Text_Times[i + 1] == null)
                        {
                            break;
                        }
                    }
                }
            }
            catch
            {
                
            }

            return LRC_Text_Temp;//整个字符串
        }

        /// <summary>
        /// 一行一行while 的读取歌词文件流中的信息并保存
        /// </summary>
        public void player_lrc_Save_Text(string SongLrcPath)
        {
/*            try//防止文件名中含有非法字符导致无法生成
            {*/
                //重新生成存储歌词逐字信息的数组
            StartTimes = new int[666];
            MiddleTimes = new int[666];
            StartKrcTimes = new int[1000];
            MiddleKrcTimes = new int[1000];
            StartKrcTexts = new string[1000];
            StartKrcTimes_All = new double[1000];

            LRC_Time = new string[9999];
            LRC_Text = new string[9999];


            Song_Lrc_StreamReader = new StreamReader(SongLrcPath, Encoding.UTF8);//完成后继续自动清理缓存

            if (Song_Lrc_StreamReader.EndOfStream == false)//指示当前流位置是否在结尾
            {
                while ((A_String_Read = Song_Lrc_StreamReader.ReadLine()) != null)
                {
                    if (A_String_Read.ToString().Length < 3)//跳过空格
                        continue;
                    if (A_String_Read.ToString().Substring(1, 2).Equals("id"))//跳过offset标签
                        continue;
                    if (A_String_Read.ToString().Substring(1, 2).Equals("ar"))//跳过ar标签
                        continue;
                    if (A_String_Read.ToString().Substring(1, 2).Equals("ti"))//跳过ti标签
                        continue;
                    if (A_String_Read.ToString().Substring(1, 2).Equals("by"))//跳过by标签
                        continue;
                    if (A_String_Read.ToString().Substring(1, 2).Equals("ha"))//跳过by标签
                        continue;
                    if (A_String_Read.ToString().Substring(1, 2).Equals("al"))//跳过by标签
                        continue;
                    if (A_String_Read.ToString().Substring(1, 2).Equals("si"))//跳过by标签
                        continue;
                    if (A_String_Read.ToString().Substring(1, 2).Equals("qq"))//跳过by标签
                        continue;
                    if (A_String_Read.ToString().Substring(1, 2).Equals("to"))//跳过by标签
                        continue;
                    if (A_String_Read.ToString().Substring(1, 2).Equals("of"))//跳过offset标签
                        continue;
                    if (A_String_Read.ToString().Substring(1, 2).Equals("la"))//跳过by标签
                        continue;

                    int nums_temp_1 = A_String_Read.IndexOf("]") + 1;
                    int nums_temp_2 = A_String_Read.IndexOf("[") + 1;

                    if (A_String_Read.Length <= nums_temp_1)//过滤歌词内容为空的行
                        continue;

                    A_String_Read = Show_Song_KRC_Text(A_String_Read);


                    //歌词空白行数
                    for (int i = LRC_Text_Null_Nums; i < 999; i++)
                    {//获取krc文件歌词内容                 
                        if (LRC_Text[i] == null)
                        {
                            if (LRC_Text_Temp != null)
                            {
                                LRC_Text[i] = A_String_Read;
                                if (LRC_Time[i] == null)
                                {
                                    LRC_Time[i] = Start_Time.ToString();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion


        //public static double Krc_Start_Times;//当前播放时间所同步的单个歌词的持续时间
        public int[] StartKrcTimes;//存储每个歌词开始的时间
        public int[] MiddleKrcTimes;//存储每个歌词持续的时间
        public string[] StartKrcTexts;//存储每个歌词的内容
        public double[] StartKrcTimes_All;//存储每个歌词开始的时间,总时间制
        public int[] StartTimes;//存储每一行歌词开始的时间
        public int[] MiddleTimes;//存储每一行歌词持续的时间
        #region 歌词行信息分类存储
        /// <summary>
        /// 存储单行歌词开始与持续的时间
        /// </summary>
        /// <param name="StartTime">存储单行歌词开始的时间</param>
        /// <param name="MiddleTime">存储单行歌词持续的时间</param>
        public void LRC_Start_And_Middle_Time(int StartTime, int MiddleTime)
        {
            for (int i = 3; i < 666; i++)
            {
                if (StartTimes[i] == 0)
                {
                    if (MiddleTimes[i] == 0)
                    {
                        StartTimes[i] = StartTime;
                        MiddleTimes[i] = MiddleTime;
                        break;
                    }
                }
            }

        }

        /// <summary>
        /// 存储每个歌词的内容
        /// </summary>
        /// <param name="StartTime">存储每个歌词开始的时间</param>
        /// <param name="MiddleTime">存储每个歌词持续的时间</param>
        /// <param name="KRC_Text">存储每个歌词的内容</param>
        public void KRC_Start_And_Middle_And_Text(int StartTime, int MiddleTime, String KRC_Text, int StartTime_ALL)
        {
            for (int i = 3; i < 1000; i++)
            {
                if (StartKrcTimes[i] == 0)
                {
                    if (MiddleKrcTimes[i] == 0)
                    {
                        if (StartKrcTexts[i] == null)
                        {
                            if (StartKrcTimes_All[i] == 0)
                            {
                                StartKrcTimes[i] = StartTime;
                                StartKrcTexts[i] = KRC_Text;
                                StartKrcTimes_All[i] = Convert.ToDouble(StartTime_ALL);

                                MiddleKrcTimes[i] = MiddleTime;

                                break;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        int krc_line_num;//计算歌词行数        
        int[] Temp_KrcTimes;//临时单行歌词中每一个歌词开始时间     
        string[] Temp_KrcText;//临时单行歌词中每一个歌词内容     
        int[] Temp_Middle_KrcTimes;//临时单行歌词中每一个歌词持续时间
        public Dao_ListBox_Temp_Krc_OneLine[] dao_ListBox_Temp_Krc_OneLine; //字符集数组（单个歌词行时间及内容 集合而成的 播放时间组）
        #region 生成歌词树状结构数组

        /// <summary>
        /// 生成歌词树状结构数组
        /// </summary>
        public void Take_TreeKrcInfo()
        {
            //计算歌词行数+3
            for (int i = 3; i < LRC_Text.Length; i++)
            {
                if (LRC_Text[i] != null)
                {
                    krc_line_num++;
                }
            }
            krc_line_num += 3;


            //<0,0,0>作<0,49,0>曲<49,102,0>：解决因时间格式造成的信息读取序列错误的问题
            for (int j = StartKrcTimes.Length - 1; j >= 3; j--)
            {
                //找出末尾不为0的歌词时间元素
                if (StartKrcTimes[j] != 0)
                {

                    for (int i = 3; i <= j; i++)
                    {
                        if (StartKrcTimes[i] == 0)
                            if (StartKrcTimes[i + 1] == 0)
                            {
                                StartKrcTimes[i + 1] = 1;//赋值为1
                            }
                    }

                    break;
                }
            }


            //[14,1035]<0,174,0>薛<174,153,0>之<327,201,0>谦 <528,152,0>- <680,203,0>暧<883,152,0>昧

            //创建字符集数组（单个歌词行时间及内容 集合而成的 播放时间组）
            dao_ListBox_Temp_Krc_OneLine = new Dao_ListBox_Temp_Krc_OneLine[krc_line_num];

            //给  字符集数组（单个歌词行时间及内容 集合而成的 播放时间组）  赋值
            for (int i = 3; i < dao_ListBox_Temp_Krc_OneLine.Length; i++)
            {
                if (dao_ListBox_Temp_Krc_OneLine[i] == null)
                {
                    //创建一行歌词信息集合  对象
                    dao_ListBox_Temp_Krc_OneLine[i] = new Dao_ListBox_Temp_Krc_OneLine();

                    dao_ListBox_Temp_Krc_OneLine[i].Krc_Line_Start_Time = StartTimes[i];
                    dao_ListBox_Temp_Krc_OneLine[i].Krc_Line_Continue_Time = MiddleTimes[i];
                    dao_ListBox_Temp_Krc_OneLine[i].Krc_Line_Text = LRC_Text[i];//"薛之谦 - 暧昧";


                    //计算歌词行字符数（含有的播放时间组）
                    //检测歌词行的单个歌词的个数
                    int Krc_Line_Text_Nums = 0;//dao_ListBox_Temp_Krc_OneLine[i].Krc_Line_Text.Trim().Length;


                    //i代表正处于歌词文件的行数


                    //当前遍历歌词数组的下标位置
                    int Zero_Nums = 11;
                    //提取这一行所有的歌词信息并分类
                    for (int j = 3; j < StartKrcTimes.Length; j++)
                    {
                        //检测到一行以0为首的数据流   0,234，3,44,5454，0,123,3432，4556
                        if (StartKrcTimes[j] == 0)
                        {
                            //检测到结果为0，代表找到一行歌词的下标索引
                            Zero_Nums++;
                        }

                        //下标索引歌词行  遍历到   当前与之对应的歌词行(i)
                        if (Zero_Nums == i)
                        {

                            //计算此单行歌词中单个歌词元素得到数量
                            for (int g = j + 1; g < StartKrcTimes.Length; g++)
                            {
                                if (StartKrcTimes[g] != 0)
                                {
                                    //识别到歌词内容
                                    Krc_Line_Text_Nums++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            Krc_Line_Text_Nums += 1;//第一个元素为0，不被遍历，故加1


                            //给中间临时歌词信息数组   赋值（单行歌词中单个歌词的开始时间，持续时间，歌词内容）
                            Temp_KrcTimes = new int[Krc_Line_Text_Nums];
                            Temp_KrcText = new string[Krc_Line_Text_Nums];
                            Temp_Middle_KrcTimes = new int[Krc_Line_Text_Nums];
                            for (int l = 0; l < Temp_KrcTimes.Length; l++)
                            {
                                if (Temp_KrcTimes[l] == 0)
                                {
                                    //从已经提取好的歌词信息集合数组中  再次提取细分
                                    Temp_KrcTimes[l] = StartKrcTimes[j + l];
                                    Temp_KrcText[l] = StartKrcTexts[j + l];
                                    Temp_Middle_KrcTimes[l] = MiddleKrcTimes[j + l];
                                }
                            }

                            break;
                        }
                    }


                    //创建字符集数组（单个歌词时间及内容 集合而成的 播放时间组）
                    dao_ListBox_Temp_Krc_OneLine[i].dao_listBox_temp_krc_oneLine_moreByte = new Dao_ListBox_Temp_Krc_OneLine_MoreByte[Krc_Line_Text_Nums];
                    //给  字符集数组（单个歌词时间及内容 集合而成的 播放时间组）  赋值
                    for (int k = 0; k < dao_ListBox_Temp_Krc_OneLine[i].dao_listBox_temp_krc_oneLine_moreByte.Length; k++)
                    {
                        if (dao_ListBox_Temp_Krc_OneLine[i].dao_listBox_temp_krc_oneLine_moreByte[k] == null)
                        {
                            //创建一行歌词信息集合  中单个歌词所含有的歌词信息   对象
                            dao_ListBox_Temp_Krc_OneLine[i].dao_listBox_temp_krc_oneLine_moreByte[k] = new Dao_ListBox_Temp_Krc_OneLine_MoreByte();

                            dao_ListBox_Temp_Krc_OneLine[i].dao_listBox_temp_krc_oneLine_moreByte[k].Krc_One_Start_Time = dao_ListBox_Temp_Krc_OneLine[i].Krc_Line_Start_Time + Temp_KrcTimes[k];//组成整首歌中歌词所处的时间戳
                            dao_ListBox_Temp_Krc_OneLine[i].dao_listBox_temp_krc_oneLine_moreByte[k].Krc_One_Continue_Time = Temp_Middle_KrcTimes[k];
                            dao_ListBox_Temp_Krc_OneLine[i].dao_listBox_temp_krc_oneLine_moreByte[k].Krc_One_End_Time = 0;//不使用，故为零
                            dao_ListBox_Temp_Krc_OneLine[i].dao_listBox_temp_krc_oneLine_moreByte[k].Krc_One_Text = Temp_KrcText[k];
                        }
                    }
                }
            }



            //重新指定最后一句歌词的持续时间，
            //[200949,227971]<0,710,0>请<3188,605,0>原<3793,353,0>谅<4146,411,0>我<4557,456,0>多<5013,408,0>情<5421,509,0>的<5930,404,0>打<6334,304,0>扰
            for (int i = dao_ListBox_Temp_Krc_OneLine.Length - 1; i >= 3; i--)
            {
                if (dao_ListBox_Temp_Krc_OneLine[i] != null)
                {
                    dao_ListBox_Temp_Krc_OneLine[i].Krc_Line_Continue_Time = 0;

                    //dao_ListBox_Temp_Krc_OneLine[i].Krc_Line_Continue_Time -= dao_ListBox_Temp_Krc_OneLine[i].Krc_Line_Start_Time;

                    for (int k = dao_ListBox_Temp_Krc_OneLine[i].dao_listBox_temp_krc_oneLine_moreByte.Length - 1; k >= 0; k--)
                    {
                        if (dao_ListBox_Temp_Krc_OneLine[i].dao_listBox_temp_krc_oneLine_moreByte[k] != null)
                        {
                            //(Krc_One_Start_Time - Krc_Line_Start_Time)最后一个歌词的开始时间 + Krc_One_Continue_Time
                            dao_ListBox_Temp_Krc_OneLine[i].Krc_Line_Continue_Time = dao_ListBox_Temp_Krc_OneLine[i].dao_listBox_temp_krc_oneLine_moreByte[k].Krc_One_Start_Time - dao_ListBox_Temp_Krc_OneLine[i].Krc_Line_Start_Time + dao_ListBox_Temp_Krc_OneLine[i].dao_listBox_temp_krc_oneLine_moreByte[k].Krc_One_Continue_Time;

                            break;
                        }
                    }

                    break;
                }
            }
        }
        #endregion

        #endregion

        /// <summary>
        /// 使用歌词字同步
        /// </summary>
        #region 歌词字同步

        public Krc_Line_Info[] krc_Line_Info;
        public static string Song_KRC_Path;//歌词文件所在的路径
        public double Start_Song_KRC_Time;
        public double End_Song_KRC_Time;
        //歌词信息类
        public ArrayList arrayList_krc_line = new ArrayList();

        /// <summary>
        /// 获取初始krc行数据
        /// </summary>
        /// <param name="SongLrcPath">歌词文件的文件路径</param>
        public void Take_TreeKrcInfo(string SongLrcPath)
        {
            try
            {
                Song_Lrc_StreamReader = new StreamReader(SongLrcPath, Encoding.UTF8);//完成后继续自动清理缓存

                if (Song_Lrc_StreamReader.EndOfStream == false)//指示当前流位置是否在结尾
                {
                    while ((A_String_Read = Song_Lrc_StreamReader.ReadLine()) != null)
                    {
                        if (A_String_Read.ToString().Length < 3)//跳过空格
                            continue;
                        if (A_String_Read.ToString().Substring(1, 2).Equals("id"))//跳过offset标签
                            continue;
                        if (A_String_Read.ToString().Substring(1, 2).Equals("ar"))//跳过ar标签
                            continue;
                        if (A_String_Read.ToString().Substring(1, 2).Equals("ti"))//跳过ti标签
                            continue;
                        if (A_String_Read.ToString().Substring(1, 2).Equals("by"))//跳过by标签
                            continue;
                        if (A_String_Read.ToString().Substring(1, 2).Equals("ha"))//跳过by标签
                            continue;
                        if (A_String_Read.ToString().Substring(1, 2).Equals("al"))//跳过by标签
                            continue;
                        if (A_String_Read.ToString().Substring(1, 2).Equals("si"))//跳过by标签
                            continue;
                        if (A_String_Read.ToString().Substring(1, 2).Equals("qq"))//跳过by标签
                            continue;
                        if (A_String_Read.ToString().Substring(1, 2).Equals("to"))//跳过by标签
                            continue;
                        if (A_String_Read.ToString().Substring(1, 2).Equals("of"))//跳过offset标签
                            continue;
                        if (A_String_Read.ToString().Substring(1, 2).Equals("la"))//跳过by标签
                            continue;
                        if (A_String_Read.ToString().Length > 222)
                            continue;

                        int nums_temp_1 = A_String_Read.IndexOf("]");
                        //int nums_temp_2 = A_String_Read.IndexOf("[") + 1;
                        if (A_String_Read.Length <= nums_temp_1)//过滤歌词内容为空的行
                            continue;

                        arrayList_krc_line.Add(A_String_Read);
                    }
                }
            }
            catch
            {

            }
            Console.WriteLine();


            Init_Krc_Info();
        }
        /// <summary>
        /// 初始化歌词行字节信息
        /// </summary>
        public void Init_Krc_Info()
        {
            krc_Line_Info = new Krc_Line_Info[arrayList_krc_line.Count];

            string temp_head_start = "";
            string temp_head_duration = "";
            string temp_text = "";

            //初始化歌词行动画时间
            for (int i = 0; i < arrayList_krc_line.Count; i++)
            {
                //[470,1271]<0,204,0>林<204,204,0>俊<408,203,0>杰 <611,203,0>- <814,203,0>Always <1017,254,0>Online
                temp_head_start = arrayList_krc_line[i].ToString();
                temp_head_duration = temp_head_start;
                temp_text = temp_head_start;

                krc_Line_Info[i] = new Krc_Line_Info();

                krc_Line_Info[i].This_Krc_Line = i + 1;
                krc_Line_Info[i].This_Krc_Start_Time = Convert.ToInt32(temp_head_start.Substring(temp_head_start.IndexOf("[") + 1, temp_head_start.IndexOf(",") - temp_head_start.IndexOf("[") - 1));
                krc_Line_Info[i].This_Krc_Duration = Convert.ToInt32(temp_head_duration.Substring(temp_head_duration.IndexOf(",") + 1, temp_head_duration.IndexOf("]") - temp_head_duration.IndexOf(",") - 1));

                //<0,204,0>林<204,204,0>俊<408,203,0>杰 <611,203,0>- <814,203,0>Always <1017,254,0>Online
                temp_text = temp_text.Substring(temp_text.IndexOf("]") + 1);
                while (true)
                {
                    //添加歌词行单个字符动画开始时间
                    if (temp_text.IndexOf(",") > temp_text.IndexOf("<"))
                        krc_Line_Info[i].Array_Morebyte_BeginTime.Add(temp_text.Substring(temp_text.IndexOf("<") + 1, temp_text.IndexOf(",") - temp_text.IndexOf("<") - 1));
                    else
                        krc_Line_Info[i].Array_Morebyte_BeginTime.Add(temp_text.Substring(temp_text.IndexOf("<") + 1, temp_text.IndexOf(",") - temp_text.IndexOf("<") - 1));

                    //添加歌词行单个字符动画持续时间
                    //204,0>林<204,204,0>俊<408,203,0>杰 <611,203,0>- <814,203,0>Always <1017,254,0>Online
                    temp_text = temp_text.Substring(temp_text.IndexOf(",") + 1);
                    krc_Line_Info[i].Array_Morebyte_Duration.Add(temp_text.Substring(0, temp_text.IndexOf(",")));

                    //添加歌词字节
                    //林<204,204,0>俊<408,203,0>杰 <611,203,0>- <814,203,0>Always <1017,254,0>Online
                    temp_text = temp_text.Substring(temp_text.IndexOf(">") + 1);
                    if (temp_text.IndexOf("<") > 0)
                        krc_Line_Info[i].Array_Morebyte_Text.Add(temp_text.Substring(0, temp_text.IndexOf("<")));
                    else
                        krc_Line_Info[i].Array_Morebyte_Text.Add(temp_text);

                    //计算歌词字节数量
                    krc_Line_Info[i].Int_MoreByte_Nums++;

                    //验证是否还存在字符
                    if (temp_text.IndexOf("<") > 0)
                        temp_text = temp_text.Substring(temp_text.IndexOf("<"));//去除已添加的歌词字节
                    else
                        break;
                }
            }

            int nums_This_Krc_Duration = 0;
            for (int i = 0; i < krc_Line_Info[krc_Line_Info.Length - 1].Int_MoreByte_Nums; i++)
            {
                nums_This_Krc_Duration += Convert.ToInt32(krc_Line_Info[krc_Line_Info.Length - 1].Array_Morebyte_Duration[i]);
            }
            krc_Line_Info[krc_Line_Info.Length - 1].This_Krc_Duration = nums_This_Krc_Duration;
            //未指定目标

            //This_Krc_Start_Time为0时定时器无法捕捉
            for (int i = 0; i < krc_Line_Info.Length; i++)
            {
                if (krc_Line_Info[i].This_Krc_Start_Time == 0)
                {
                    krc_Line_Info[i].This_Krc_Start_Time = 222;
                }
            }

            //Duration重新设置为krc_Line_Info[i].This_Krc_Duration
            for (int i = 0; i < krc_Line_Info.Length; i++)
            {
                int Duration_Line = 
                    Convert.ToInt32(krc_Line_Info[i].Array_Morebyte_BeginTime[
                        krc_Line_Info[i].Array_Morebyte_BeginTime.Count - 1])
                    + Convert.ToInt32(krc_Line_Info[i].Array_Morebyte_Duration[
                        krc_Line_Info[i].Array_Morebyte_Duration.Count - 1]);
                krc_Line_Info[i].This_Krc_Duration = Duration_Line;
            }
        }

        #endregion


        /// <summary>
        /// 获取歌词信息方法集合
        /// </summary>
        #region 获取歌词信息方法集合

        Dao_ListBox_Temp_KRC_Bing[] dao_ListBox_Temp_KRC_Bing;
        /// <summary>
        /// 返回ListBox所包含的歌词文本
        /// </summary>
        /// <returns>数据绑定,返回ListBox所包含的歌词文本</returns>
        public Dao_ListBox_Temp_KRC_Bing[] Return_ListBox_Temp_KRC_Bing()
        {
            dao_ListBox_Temp_KRC_Bing = new Dao_ListBox_Temp_KRC_Bing[9999];

            //去除歌词行中的空格，防止歌词动画分段显示
            //Trim()，去除字符串前后的指定字符
            //Replace()，
            for (int i = 0; i < LRC_Text.Length; i++)
            {
                if (LRC_Text[i] != null)
                {
                    //检测到韩文，需要去除空格，不然TextBlock动画会分段显示
                    if (System.Text.RegularExpressions.Regex.IsMatch(LRC_Text[i].Replace(" ", "").ToString(), @"^[\uac00-\ud7ff]+$")){
                        //韩文
                        LRC_Text[i] = LRC_Text[i].Replace(" ", "");
                    }
                    /*
                     * char[] temp = LRC_Text[i].ToCharArray();
                    for (int j = 0; j < temp.Length; j++)
                    {
                        //检测到韩文就删去空格
                        if (System.Text.RegularExpressions.Regex.IsMatch(temp[j].ToString(), @"^[\uac00-\ud7ff]+$"))                    
                        {
                            //韩文
                            LRC_Text[i] = LRC_Text[i].Replace(" ", "");
                            break;
                        }
                    }
                     */
                }
            }

            int count = 0;
            for (int i = 0; i < LRC_Text.Length; i++)
            {
                if (dao_ListBox_Temp_KRC_Bing[i] == null)
                {
                    if (LRC_Text[i] != null)
                    {
                        Dao_ListBox_Temp_KRC_Bing temp = new Dao_ListBox_Temp_KRC_Bing();

                        temp.Song_KRC_Line = LRC_Text[i];

                        dao_ListBox_Temp_KRC_Bing[i] = temp;

                        count++;
                    }
                }
            }

            count += LRC_Text_Null_Nums * 2;//前后需要3，22行空行
            Dao_ListBox_Temp_KRC_Bing[] Temp_KRC_Bing = new Dao_ListBox_Temp_KRC_Bing[count];

            for (int i = LRC_Text_Null_Nums; i < dao_ListBox_Temp_KRC_Bing.Length - LRC_Text_Null_Nums; i++)
            {
                if (dao_ListBox_Temp_KRC_Bing[i] != null)
                {
                    if (Temp_KRC_Bing[i] == null)
                    {
                        Temp_KRC_Bing[i] = dao_ListBox_Temp_KRC_Bing[i];
                    }
                }
            }

            //可解决当listViewItem为Null时，无法滚动至底部的问题
            for (int i = 0; i < Temp_KRC_Bing.Length; i++)
            {
                if (Temp_KRC_Bing[i] == null)
                {
                    Dao_ListBox_Temp_KRC_Bing temp = new Dao_ListBox_Temp_KRC_Bing();

                    temp.Song_KRC_Line = "       ";

                    Temp_KRC_Bing[i] = temp;
                }
            }

            return Temp_KRC_Bing;
        }


        /// <summary>
        /// 返回ListBox所包含的歌词内容
        /// </summary>
        /// <returns>返回ListBox所包含的歌词内容</returns>
        public string[] Return_ListBox_Temp_KRC_Text()
        {
            string[] temp_krc = new string[LRC_Text.Length];

            int count = 0;
            for (int i = 0; i < LRC_Text.Length; i++)
            {
                if (temp_krc[i] == null)
                {
                    if (LRC_Text[i] != null)
                    {
                        temp_krc[i] = LRC_Text[i];

                        count++;
                    }
                }
            }

            count += 14;//前后需要3，3行空行
            string[] Temp_KRC_Bing = new string[count];

            for (int i = LRC_Text_Null_Nums; i < temp_krc.Length - LRC_Text_Null_Nums; i++)
            {
                if (temp_krc[i] != null)
                {
                    if (Temp_KRC_Bing[i] == null)
                    {
                        Temp_KRC_Bing[i] = temp_krc[i];
                    }
                }
            }

            //可解决当listViewItem为Null时，无法滚动至底部的问题
            for (int i = 0; i < Temp_KRC_Bing.Length; i++)
            {
                if (Temp_KRC_Bing[i] == null)
                {
                    Temp_KRC_Bing[i] = "       ";
                }
            }


            return Temp_KRC_Bing;
        }
        /// <summary>
        /// 返回ListBox所包含的歌词时间
        /// </summary>
        /// <returns>返回ListBox所包含的歌词时间</returns>
        public double[] Return_ListBox_Temp_KRC_Time()
        {
            double[] temp = new double[LRC_Time.Length];

            for (int i = 0; i < LRC_Time.Length; i++)
            {
                if (LRC_Time[i] != null)
                {
                    if (temp[i] == 0)
                    {
                        temp[i] = Convert.ToDouble(LRC_Time[i]);
                    }
                }
            }

            return temp;
        }
        /// <summary>
        /// 生成这首歌词第一句歌词开始的时间
        /// </summary>
        /// <returns></returns>
        public double Return_Start_Song_KRC_Time()
        {
            ///生成这首歌词第一句歌词开始的时间和最后一句歌词开始的时间
            for (int j = 0; j < LRC_Time.Length; j++)//输出数组一开始的两个时间
                if (LRC_Time[j] != null)
                {
                    string temp_5 = LRC_Time[j];//753 
                    Start_First_Time_1 = Convert.ToInt64(temp_5);
                    break;
                }

            //0无法捕捉
            if (Start_First_Time_1 == 0)
                Start_First_Time_1 = 222;

            return Start_First_Time_1;
        }
        /// <summary>
        /// 生成这首歌词最后一句歌词开始的时间
        /// </summary>
        /// <returns></returns>
        public double Return_End_Song_KRC_Time()
        {
            for (int j = LRC_Time.Length - 1; j >= 0; j--)//输出数组最后的两个时间
                if (LRC_Time[j] != null)
                {
                    string temp_5 = LRC_Time[j];//201538
                    End_Last_Time_1 = Convert.ToInt64(temp_5);//毫秒数 / 100
                    break;
                }

            return End_Last_Time_1;
        }
        /// <summary>
        /// 生成每个歌词开始的时间,总时间制
        /// </summary>
        /// <returns></returns>
        public double[] Return_StartKrcTimes_All()
        {
            if (StartKrcTimes_All != null)
            {
                return StartKrcTimes_All;
            }
            return null;
        }
        /// <summary>
        /// 生成每个歌词持续的时间
        /// </summary>
        /// <returns></returns>
        public int[] Return_MiddleKrcTimes()
        {
            if (StartKrcTimes_All != null)
            {
                return MiddleKrcTimes;
            }
            return null;
        }

        #endregion
    }
}
