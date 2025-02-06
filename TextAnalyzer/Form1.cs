using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextAnalyzer
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource _cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
        }
        //Использую async для выполнения длительной операции анализа текста в фоновом потоке.
        private async void analyzeButton_Click(object sender, EventArgs e)
        {
            // Инициализация токена отмены
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            try
            {
                // Запуск анализа текста
                var report = await Task.Run(() => AnalyzeText(inputTextBox.Text, token), token);
                reportTextBox.Text = report;
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Анализ был отменен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            // Отмена анализа
            _cancellationTokenSource?.Cancel();
        }
        private string AnalyzeText(string text, CancellationToken token)
        {
            // Проверка на отмену
            token.ThrowIfCancellationRequested();

            int sentenceCount = 0;
            int wordCount = 0;
            int characterCount = text.Length;
            int questionCount = 0;
            int exclamationCount = 0;

            // Разделение текста на предложения
            var sentences = text.Split(new[] {'.','!','?'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var sentence in sentences)
            {
                // Проверка на отмену
                token.ThrowIfCancellationRequested();

                // Увеличиваем счетчик предложений
                sentenceCount++;

                // Подсчет слов
                var words = sentence.Split(new[] {' ','\n','\r'}, StringSplitOptions.RemoveEmptyEntries);
                wordCount += words.Length;

                // Проверяем, какое это предложение
                string trimmedSentence = sentence.Trim();
                if (trimmedSentence.EndsWith("?"))
                    {
                        questionCount++;
                    }

                if (trimmedSentence.EndsWith("!"))
                    {
                        exclamationCount++;
                    }
            }
            /*
            foreach (var sentence in sentences)
            {
                // Проверка на отмену
                token.ThrowIfCancellationRequested();

                if (sentence.EndsWith("?"))
                    {
                        questionCount++;
                    }

                if (sentence.EndsWith("!"))
                    {
                        exclamationCount++;
                    } 

                // Подсчет слов
                var words = sentence.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                wordCount += words.Length;
            }*/

            // Формирование отчета
            StringBuilder reportBuilder = new StringBuilder();
            reportBuilder.AppendLine($"Количество предложений: {sentenceCount}");
            reportBuilder.AppendLine($"Количество символов: {characterCount}");
            reportBuilder.AppendLine($"Количество слов: {wordCount}");
            reportBuilder.AppendLine($"Количество вопросительных предложений: {questionCount}");
            reportBuilder.AppendLine($"Количество восклицательных предложений: {exclamationCount}");

            return reportBuilder.ToString();
        }
    }
}
