using TaskFlow.Console;

var manager = new TaskManager();
bool running = true;

while (running)
{
    Console.WriteLine("\n=== TaskFlow - Gerenciador de Tarefas ===");
    Console.WriteLine("1. Adicionar Tarefa");
    Console.WriteLine("2. Listar Tarefas");
    Console.WriteLine("3. Concluir Tarefa");
    Console.WriteLine("0. Sair");
    Console.Write("Escolha uma opção: ");

    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            Console.Write("Digite o título da tarefa: ");
            var title = Console.ReadLine();
            try
            {
                var task = manager.AddTask(title ?? string.Empty);
                Console.WriteLine($"[SUCESSO] Tarefa '{task.Title}' criada com ID: {task.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] {ex.Message}");
            }
            break;

        case "2":
            Console.WriteLine("\n--- Lista de Tarefas ---");
            if (manager.Tasks.Count == 0)
            {
                Console.WriteLine("Nenhuma tarefa cadastrada.");
            }
            else
            {
                foreach (var item in manager.Tasks)
                {
                    var status = item.IsCompleted ? "[X] Concluída" : "[ ] Pendente";
                    Console.WriteLine($"{status} - {item.Title} (ID: {item.Id})");
                }
            }
            break;

        case "3":
            Console.Write("Digite o ID da tarefa a concluir: ");
            var idInput = Console.ReadLine();
            if (Guid.TryParse(idInput, out var id))
            {
                bool success = manager.CompleteTask(id);
                if (success)
                    Console.WriteLine("[SUCESSO] Tarefa concluída!");
                else
                    Console.WriteLine("[ERRO] Tarefa não encontrada.");
            }
            else
            {
                Console.WriteLine("[ERRO] ID inválido.");
            }
            break;

        case "0":
            running = false;
            Console.WriteLine("Saindo do TaskFlow... Até logo!");
            break;

        default:
            Console.WriteLine("[ERRO] Opção inválida. Tente novamente.");
            break;
    }
}