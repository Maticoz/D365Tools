using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FixLabels
{
    class Program
    {
        string line;
        string text = "";
        string outputVal = "";

        String[] Labels;
        List<string> uniqueLabels = new List<string>();
        List<string> uniqueOutput = new List<string>();
        List<DuplicateLabel> duplicateLabels = new List<DuplicateLabel>();

        public int findLabelIdInList(string _labelId)
        {
            foreach (DuplicateLabel label in duplicateLabels)
            {
                if (label.labelId == _labelId)
                {
                    return duplicateLabels.IndexOf(label);
                }
            }
            return -1;
        }

        public int findLabelConflictIdInList()
        {
            foreach (DuplicateLabel label in duplicateLabels)
            {
                if (label.labelList.Count > 1 && label.label == null)
                {
                    return duplicateLabels.IndexOf(label);
                }
            }
            return -1;
        }

        [STAThreadAttribute]
        static void Main(string[] args)
        {
            Program program = new Program();
            program.run();
        }



        public void run()
        {

            Console.WriteLine("Wklej zawartość labelki a następnie enter:");

            this.readData();
            this.init();

            Console.Clear();
            Console.WriteLine("Odczytano.. Kliknij by kontynouowac");
            Console.ReadKey();

            this.resolveConflict();

            this.outputClip();
            Console.WriteLine("Wartość została skopiowana do schowka/pamieci podrecznej");

            Console.ReadKey();
        }

        public void resolveConflict()
        {
            int indexOf, choosed, idx;

            for (int i = 0; i < this.duplicateLabels.Count; i++)
            {
                idx = i;
                DuplicateLabel labelx = this.duplicateLabels[idx];

                if (labelx.label == null && labelx.labelList.Count == 1)
                {
                    labelx.label = labelx.labelList[0];
                    this.duplicateLabels[idx] = labelx;
                }
            }


            while ((indexOf = this.findLabelConflictIdInList()) != -1)
            {
                Console.Clear();
                Console.WriteLine("Wybierz poprawna labelke:");
                idx = 1;
                foreach (string label in this.duplicateLabels[indexOf].labelList)
                {
                    Console.WriteLine($"{idx}. {label}");
                    idx++;
                }

                string readedLine = Console.ReadLine();
                choosed = Convert.ToInt32(readedLine);
                Console.WriteLine($"Wybrano {choosed} - {this.duplicateLabels[indexOf].labelList[choosed - 1]} - nacisnij enter zeby kontynuowac");

                this.duplicateLabels[indexOf].label = this.duplicateLabels[indexOf].labelList[choosed - 1];

                Console.ReadKey();
            }

        }

        public void readData()
        {
            text = "";

            while ((line = Console.ReadLine()) != "")
            {
                text += line + "\n";
            }
        }

        public void outputClip()
        {
            outputVal = "";
            foreach (DuplicateLabel uniqueOut in this.duplicateLabels)
            {
                outputVal += uniqueOut.label + "\n";
            }
            Clipboard.SetText(outputVal);
        }

        public void init()
        {
            this.initLabels();
            this.fillDuplicates();
        }

        public void initLabels()
        {
            Labels = text.Split('\n');
        }

        public void fillDuplicates()
        {
            int indexOf;
            DuplicateLabel duplicateLabel = null;
            foreach (String label in Labels)
            {
                string[] labelSplited = label.Split(new[] { '=' }, 2);

                if (labelSplited.Length != 2)
                    continue;

                string labelId = labelSplited[0];
                string labelVal = labelSplited[1];

                indexOf = this.findLabelIdInList(labelId);

                if (indexOf == -1)
                {
                    duplicateLabel = new DuplicateLabel(labelId);
                    this.duplicateLabels.Add(duplicateLabel);
                    duplicateLabel.addLabel(label);
                }
                else
                {
                    duplicateLabel = duplicateLabels[indexOf];
                    duplicateLabel.addLabel(label);
                    this.duplicateLabels[indexOf] = duplicateLabel;
                }

            }
        }
    }
}