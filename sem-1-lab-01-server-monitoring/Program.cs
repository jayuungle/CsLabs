using System;

namespace Lab_1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Запуск панели администратора");
            Console.Write("Введите имя администратора: ");
            string adm_name = Console.ReadLine();
            Console.Write("Введите пароль администратора: ");
            int adm_pass = Convert.ToInt32(Console.ReadLine());
            if (adm_pass != 67)
            {
                Console.WriteLine("Ошибка аутентификации. Завершение работы...");
                return;
            }
            
            Console.WriteLine($"Добро пожаловать, {adm_name}!\n");
            Console.WriteLine("Загрузка характеристик сервера...");
            
            string srv_name = "Counter-Strike";
            string current_map = "de_mirage"; 
            ushort players_online = 25;
            ushort max_players = 67;
            ushort tick_rate = 128;
            float server_fps = 250.5f;
            double cpu_load = 42.75;
            bool is_vac_active = true;

            string buffer = "15";
            int ping = Convert.ToInt32(buffer);

            int free_slots = max_players - players_online; 

            Console.WriteLine("\n\t+------------------------------------------+");
            Console.WriteLine("\t|             СВОДКА СЕРВЕРА               |");
            Console.WriteLine("\t+------------------------------------------+");
            Console.WriteLine($"\t| {"Название:", -21} | {srv_name, -16} |");
            Console.WriteLine($"\t| {"Текущая карта:", -21} | {current_map, -16} |");
            Console.WriteLine("\t+------------------------------------------+");
            Console.WriteLine($"\t| {"Игроков онлайн:", -21} | {players_online + " из " + max_players, -16} |");
            Console.WriteLine($"\t| {"Сетевой пинг:", -21} | {ping + " мс", -16} |");
            Console.WriteLine($"\t| {"Рейтинг:", -21} | {tick_rate + " Hz", -16} |");
            Console.WriteLine($"\t| {"Свободных слотов:", -21} | {free_slots, -16} |");
            Console.WriteLine("\t+------------------------------------------+");
            Console.WriteLine($"\t| {"Серверный FPS:", -21} | {server_fps, -16} |");
            Console.WriteLine($"\t| {"Загрузка CPU:", -21} | {cpu_load + "%", -16} |");
            Console.WriteLine($"\t| {"Античит VAC:", -21} | {(is_vac_active ? "Вкл" : "Выкл"), -16} |");
            Console.WriteLine("\t+------------------------------------------+");

            Console.WriteLine("\n Нажмите клавишу Enter для выхода...");
            Console.ReadLine();
         }
    }
}