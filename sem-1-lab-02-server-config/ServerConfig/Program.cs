using System;

namespace ServerConfig
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("===== Проверка конфигурации сервера =====");

            string verdict = CheckConfiguration(64, 8, true, false);
            Console.WriteLine($"Вердикт: {verdict}");

            Console.ReadLine();
        }

        public static string CheckConfiguration(int max_players, int ram, bool is_public, bool has_password)
        {
            if (max_players <= 0)
            {
                return $"Запуск невозможен: количество игроков должно быть больше нуля. Указано: {max_players}." +
                       "Серверу необходим хотя бы один слот для подключения.";
            }

            if (ram <= 1)
            {
                return $"Запуск невозможен: серверу недостаточно оперативной памяти. Выделено: {ram} ГБ." +
                       "Для стабильной загрузки ядра операционной системы и серверного модуля требуется более 1 ГБ RAM.";
            }

            if (is_public && has_password)
            {
                return "Запуск возможен с предупреждением: публичный сервер защищен паролем." +
                       "Сервер отображается в общем глобальном поиске, но обычные пользователи не смогут на него зайти без ввода пароля.";
            }

            if (max_players >= 100 && ram <=4)
            {
                return $"Запуск возможен с предупреждением: для такого количества игроков рекомендуется больше оперативной памяти." +
                       $"Вы выделили всего {ram} ГБ RAM под нагрузку в {max_players} слотф(ов). Возможны сильные зависания и падение сервера при пиковом онлайне.";
            }

            return $"Сервер готов к запуску. Конфигурация оптимальна: {max_players} слота(ов) обеспечены {ram} ГБ оперативной памяти.";
        }
    }
}