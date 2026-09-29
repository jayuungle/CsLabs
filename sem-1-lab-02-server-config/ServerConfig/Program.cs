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

        public static string CheckConfiguration(int max_players, int memory, bool is_public, bool has_password)
        {
            if (max_players <= 0)
            {
                return $"Запуск невозможен: количество игроков должно быть больше нуля.";
            }

            if (memory <= 1)
            {
                return $"Запуск невозможен: серверу недостаточно оперативной памяти. Выделено: {memory} ГБ." +
                       "Для запуска нужно не менее 1 ГБ оперативной памяти.";
            }

            if (is_public && has_password)
            {
                return "Сервер запущен в режиме ограниченного доступа (защищен паролем)" +
                       "Сервер виден в глобальном поиске, но для подключения нужен пароль";
            }

            if (max_players >= 100 && memory <=4)
            {
                return $"Запуск возможен с предупреждением: для текущего количества игроков рекомендуется выделить больше оперативной памяти." +
                       $"Для нагрузки в {max_players} игроков выделено всего {memory} ГБ памяти. Это может привести к критическим зависаниям или остановке сервера при максимальном онлайне.";
            }

            return $"Сервер готов к запуску. Конфигурация оптимальна: {max_players} слота(ов) обеспечены {memory} ГБ оперативной памяти.";
        }
    }
}