namespace CustomPost2023.Data.Calculations;

using CustomPost2023.Data.Models;

public class AppToStaff
{

    private static void GetStaff(int custom_post_id)
    {
        using (var context = new ApplicationContext())
        {
            int customPostIdToCheck = custom_post_id; // Замените этим значением на то, которое вы хотите проверить

            int count = context.application.Count(app => app.custom_post_id == customPostIdToCheck);

            Console.WriteLine($"Количество строк с custom_post_id равным {customPostIdToCheck}: {count}");
        }
    }

    static int CountApplicationsByCustomPostId(ApplicationContext context, int customPostId)
    {
        return context.application.Count(app => app.custom_post_id == customPostId);
    }

    public static void GetCount()
    {
        using (var context = new ApplicationContext())
        {
            var postCounts = context.staff
                .GroupBy(s => s.custom_post_id)
                .Select(g => new { CustomPostId = g.Key, Count = g.Count() })
                .ToList();

            Console.WriteLine("Количество записей для каждого значения в столбце custom_post_id:");

            foreach (var postCount in postCounts)
            {
                Console.WriteLine($"custom_post_id: {postCount.CustomPostId}, Count: {postCount.Count}");
            }
        }
    }

    static List<int> GetStaffIdsByCustomPostId(ApplicationContext context, int customPostId)
    {
        return context.staff
            .Where(staffMember => staffMember.custom_post_id == customPostId)
            .Select(staffMember => staffMember.id)
            .ToList();
    }


    static int GetStaffIdWithMinApplicationCount(ApplicationContext context, List<int> staffIds)
    {
        var staffWithMinApplicationCount = context.application
            .Where(app => staffIds.Contains(app.staff_id))
            .GroupBy(app => app.staff_id)
            .Select(group => new
            {
                StaffId = group.Key,
                ApplicationCount = group.Count()
            })
            .OrderByDescending(item => item.ApplicationCount) // Используем OrderByDescending для сортировки по убыванию
            .Last();

        return staffWithMinApplicationCount?.StaffId ?? 0; // Возвращает 0, если не найдено
    }

    static public int DisplayApplicationCountByStaffId(ApplicationContext context, int customPostId)
    {
        var staffsWithApplicationCount = from staff in context.staff
            join app in context.application
                on staff.id equals app.staff_id into staffApplications
            where staff.custom_post_id == customPostId
            select new
            {
                StaffId = staff.id,
                ApplicationCount = staffApplications.Count()
            };

        Console.WriteLine(
            $"Количество application_id для каждого staff_id для custom_post_id {customPostId} (включая 0):");
        int min = 1000;
        foreach (var item in staffsWithApplicationCount)
        {
            if (item.ApplicationCount < min)
            {
                min = item.StaffId;
            }

            Console.WriteLine($"Staff ID: {item.StaffId}, Application Count: {item.ApplicationCount}");
        }

        // Добавим логику вывода Staff ID с наименьшим количеством application_id
        int staffIdWithMinApplicationCount =
            GetStaffIdWithMinApplicationCount(context, staffsWithApplicationCount.Select(s => s.StaffId).ToList());
        return min;
    }
}