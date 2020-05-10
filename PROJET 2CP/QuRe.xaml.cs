using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace PROJET_2CP
{
    /// <summary>
    /// Logique d'interaction pour QuRe.xaml
    /// </summary>
    public partial class QuRe : Page
    {
        private int langue = MainWindow.langue;
        private int last = 1;
        public QuRe()
        {
            InitializeComponent();
            creerLesson();
            configurerLangue();
        }
        private void configurerLangue()
        {

            if(langue==0)
            {
                conseil.Text = "Essayez de deviner la réponse tout seul avant de la consulter";
                astuce.Text = "Cliquez sur la question pour obtenir la réponse";
                ex1.Text = "De quels facteurs dépend la distance de sécurité ";
                ex2.Text = "Quels sont les dangers liés la conduite par temps de pluie forte ";
                ex3.Text = "Expliquer la procédure de <<retrait immédiat du permis de conduite avec effet suspensif>>.Citez 5 cas. ";
                ex4.Text = "Quelles sont les manœuvres interdites sur autoroute ? ";
                ex5.Text = "Que peut-on conduire avec la catégorie<<B>> ? ";
                ex6.Text = "Comment sont délimitées les bandes d'arrêt d'urgences ? Peut-on franchir ces lignes ? ";
                tb1.Text = "Cette distance dépend de la vitesse , elle est d'autant plus grande que la vitesse est élevée.Elle dépend aussi de l'état de la route (sèche ou mouillée),la distance doit être doublée lorsque la chaussée est mouillée. ";
                tb2.Text = "Les dangers liés à la conduite par temps de pluie forte sont : -L'adhérence des pneus sur le sol est réduite. - Les flaques d'eau qui se forment peuvent freiner dangereusement le véhicule. - Les pneus qui n'arrivent plus à évacuer l'eau se mettent à glisser (aquaplaning). - Les distances de freinage et d'arrêt sont plus allongées. - Les roues se bloquent plus facilement lors d'un freinage ";
                tb3.Text = "Le retrait du permis de conduite est retiré immédiatement par les agent habilités. En attendant sa comparution devant la commission de retrait de permis de conduite , il est remis au conducteur , séance tenante,un procés verbal de notfication attestant la mesure de retrait immédiat qui prend effet à l'issue d'un delai de 48 heures par rapport à la date de notfication (délai maximum d'acheminement et de stationnement du véhicule) ";
                tb4.Text = "Les essais de véhicules à moteur ou de châssis,les courses,épreuves ou compétition sportives sont interdits. Il est interdit aux véhicules de pénétrer ou de séjourner sur la bande centrale séparative des chaussées. Il est interdit de faire demi-tour , notamment  en traversant la bande central séparative des chaussées ou en empruntant une interruption de celle-ci . il est interdit de faire marche arrière. On doit jamais dépasser par la droite un usager qui s'entête à rouler sur une voie située à gauche . Sauf cas  de nécessité absolue ,l'arrêt et le stationnement sont interdits sur les chaussées et les accotements , notamment les bandes d'arrêt d'urgence.Cette interdiction s'étend également aux bretelles de raccordement de l'autoroute. La circulation sur les bandes d'arrêt d'urgence est interdite. Il est interdit aux conducteurs des véhicules de transport de personnes ou de marchandises dont la longueur dépasse 7 métres ou dont le PTAC est supérieur à 2 tonnes de circuler sur la voie immédiatement situés à gauche dans le cas d'une route à 3 voies ou plus affectées à un même sens de circulation (Article L.72(9)). Par exception,ces règles ne s'appliquent pas aux véhicules prioritaires lorsqu'ils se trouvent ou se rendent en lieu où leur intervention est nécessaire ainsi qu'aux ambulances lorsqu'elles circulent pour effectuer ou un transport d'urgence de malade ou de blessé.    ";
                tb5.Text = "Avec la catégorie <<B>> , on peut conduire : Les véhicules automobiles ayant un PTAC qui n'exède pas 3,5 tonnes,affectés au transport de personnes et comportant , outre le siège du conducteur , huit places assises au maximum. Les véhicules automobiles ayant un PTAC qui n'excède pas 3,5 tonnes ,affectés au transport de marchandise (camionnettes). Aux véhicules de cette catégorie , peut être attelée une remorque (une caravane) dont le PTAC ne dépasse pas 750 Kg à condition que le total des deux PTAC n'excède pas 3,5 tonnes.  ";
                tb6.Text = "Les bandes d'arrêt d'urgence sont délimitées par des lignes longitudinales discontinues(20m/6m) que seuls les vehicules prioritaire peuvent franchir .La circulation des autres véhicules y est interdite.L'arrêt et le stationnement sont également interdits sauf cas de nécessité absolue.";

            }
            if(langue==1)
            {
                conseil.Text = "حاول أن تخمن الإجابة بنفسك ";
                astuce.Text = "انقر فوق السؤال للحصول على الإجابة";
                ex1.Text = "ما العوامل التي تعتمد على مسافة الأمان";
                ex2.Text = "ما هي مخاطر القيادة في الأمطار الغزيرة";
                ex3.Text = "اشرح إجراء <<السحب الفوري لرخصة القيادة مع تأثير التعليق>>. اذكر 5 حالات.";
                ex4.Text = "ما هي المناورات المحظورة على الطريق السريع؟";
                ex5.Text = "ما الذي يمكن فعله بالفئة<<B>>>؟";
                ex6.Text = "كيف يتم تعريف نطاقات التوقف في حالة الطوارئ؟ هل يمكننا عبور هذه الخطوط؟";
                tb1.Text = "تعتمد هذه المسافة على السرعة، وهي أكبر عندما تكون السرعة عالية. كما يعتمد على حالة الطريق (جاف أو رطب)، ويجب مضاعفة المسافة عندما يكون الطريق مبتلاً.";
                tb2.Text = "مخاطر القيادة في المطر الغزير هي: - تقليل قبضة الإطارات على الأرض. - يمكن أن تؤدي البرك التي تتكون إلى كبح المركبة بشكل خطير. - الإطارات التي لم تعد قادرة على تصريف الماء تبدأ في الانزلاق (الانزلاق على سطح الماء). - مسافات الفرملة والتوقف أطول. - يتم قفل العجلات بسهولة أكبر عند الفرملة";
                tb3.Text = "ويقوم الموظفون المخوّلة بسحب رخصة القيادة على الفور. وفي انتظار مثوله أمام مجلس إبطال رخصة القيادة، يسلم إلى السائق، أثناء الدورة، إجراء شفهي للتوثيق يشهد على إجراء الانسحاب الفوري الذي يبدأ سريانه بعد تأخير 48 ساعة مقارنة بتاريخ التوثيق (الحد الأقصى لوقت نقل السيارة وموقفيها)";
                tb4.Text = "يحظر اختبار السيارات أو الهياكل أو السباقات أو الأحداث الرياضية أو المسابقات. يحظر دخول المركبات أو البقاء على الشريط المركزي الذي يفصل الطرق. يحظر الانعطاف بشكل خاص عن طريق عبور الشريط المركزي الذي يفصل بين الطرق أو عن طريق استعارة المقاطعة. يحظر الانعكاس. يجب ألا تمرر أبدًا مستخدمًا يقود بعناد في ممر على اليسار إلى اليمين. يحظر التوقف ووقوف السيارات على الطرق والكتفين ، بما في ذلك ممرات التوقف في حالات الطوارئ ، ما لم يكن ذلك ضروريًا ، ويمتد هذا الحظر أيضًا إلى منحدرات توصيل الطرق السريعة. يحظر القيادة في ممرات الطوارئ. يحظر على سائقي المركبات التي تنقل الأشخاص أو البضائع التي يزيد طولها عن 7 أمتار أو يزيد وزنها الإجمالي عن 2 طن من القيادة على الحارة الموجودة مباشرة على اليسار في حالة وجود طريق يحتوي على 3 حارات أو أكثر المخصصة لنفس اتجاه الحركة (المادة L.72 (9)). على سبيل الاستثناء ، لا تنطبق هذه القواعد على المركبات ذات الأولوية عندما تكون في أو تذهب إلى مكان يكون فيه تدخلها ضروريًا ، وكذلك على سيارات الإسعاف عندما تكون متداولة لإحداث أو نقل الطوارئ للمرضى أو المصابين .";
                tb5.Text = "مع الفئة B >> ، يمكنك القيادة: السيارات ذات الوزن الإجمالي الذي لا يتجاوز 3.5 طن ، وتستخدم لنقل الركاب وتشمل ، بالإضافة إلى مقعد السائق ، ثمانية مقاعد كحد أقصى. السيارات التي لا تتجاوز حمولتها 3.5 طن ، وتستخدم لنقل البضائع (الشاحنات). بالنسبة للمركبات من هذه الفئة ، يمكن إرفاق مقطورة (قافلة) بوزن إجمالي للمركبة يبلغ 750 كجم ، بشرط ألا يتجاوز إجمالي المركبتين الإجماليتين الأقصى 3.5 طن.";
                tb6.Text = "يتم حدود نطاقات التوقف في حالة الطوارئ بواسطة خطوط طولية غير متصلة (20 م/6 م) والتي يمكن تجاوزها بواسطة السيارات ذات الأولوية فقط. يحظر حركة المركبات الأخرى. كما يحظر التوقف أو إيقاف السيارة إلا إذا لزم الأمر.";
            }
        }
        private void creerLesson()
        {
            Button b = new Button();
            b.Height = 60;
            b.Width = 150;
            b.Margin = new Thickness(10, 0, 10, 0);
            b.Background = Brushes.SkyBlue;
            b.BorderBrush = null;
            b.Tag = 1;
            b.Click += Button_Click;
            if (langue == 0)
            {
                b.Content = "Test 1";
            }
            if (langue == 1)
            {
                b.Content = "اختبار 1";
            }
            sp.Children.Add(b);

            for (int i = 2; i <= 16; i++)
            {
                b = new Button();
                b.Height = 60;
                b.Width = 150;
                b.Margin = new Thickness(10, 0, 10, 0);
                b.Background = Brushes.DeepSkyBlue;
                b.BorderBrush = null;
                b.Tag = i;
                b.Click += Button_Click;
                if(langue==0)
                {
                    b.Content = "Test " + i.ToString();
                }
                if(langue==1)
                {
                    b.Content = "اختبار " + i.ToString();
                }
                sp.Children.Add(b);
               
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (object child in sp.Children)
            {
               if((int)((Button)child).Tag==last)
                {
                    ((Button)child).Background = Brushes.DeepSkyBlue;
                }
            }
            ((Button)sender).Background = Brushes.SkyBlue;
           
            e1.IsExpanded = false;
            e2.IsExpanded = false;
            e3.IsExpanded = false;
            e4.IsExpanded = false;
            e5.IsExpanded = false;
            e6.IsExpanded = false;

            var index = (int)((Button)sender).Tag;
            last = index;
            SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from [QuestRep] where section ='" + Convert.ToString(index) + "'", connection);
            connection.Open();
            DataTable dt = new DataTable();
             DataRow r;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            r = dt.Rows[0];
            if (langue == 1)
            {
                ex1.Text = r[3].ToString() ;
                tb1.Text = r[4].ToString();
            }
            if (langue == 0)
            {
                ex1.Text = r[1].ToString();
                tb1.Text = r[2].ToString();
            }
            r = dt.Rows[1];
            if (langue == 1)
            {
                ex2.Text = r[3].ToString();
                tb2.Text = r[4].ToString();
            }
            if (langue == 0)
            {
                ex2.Text = r[1].ToString();
                tb2.Text = r[2].ToString();
            }
            r = dt.Rows[2];
            if (langue == 1)
            {
                ex3.Text = r[3].ToString();
                tb3.Text = r[4].ToString();
            }
            if (langue == 0)
            {
                ex3.Text = r[1].ToString();
                tb3.Text = r[2].ToString();
            }
            r = dt.Rows[3];
            if (langue == 1)
            {
                ex4.Text = r[3].ToString();
                tb4.Text = r[4].ToString();
            }
            if (langue == 0)
            {
                ex4.Text = r[1].ToString();
                tb4.Text = r[2].ToString();
            }
            r = dt.Rows[4];
            if (langue == 1)
            {
                ex5.Text = r[3].ToString();
                tb5.Text = r[4].ToString();
            }
            if (langue == 0)
            {
                ex5.Text = r[1].ToString();
                tb5.Text = r[2].ToString();
            }
            r = dt.Rows[5];
            if (langue == 1)
            {
                ex6.Text = r[3].ToString();
                tb6.Text = r[4].ToString();
            }
            if (langue == 0)
            {
                ex6.Text = r[1].ToString();
                tb6.Text = r[2].ToString();
            }
            connection.Close();
        }

        private void backclick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new PagePrincipale();
        }
    }
}
