/*
Input:
4 1
3 4
1 1000
2 2
5 6

Output:
2060
*/

#include <iostream>
#include <cstdio>
#include <queue>

class Dragon
{
public:
    int Days, Fee;
    double Priority;

    Dragon(int days, int fee)
    {
        Days = days;
        Fee = fee;
        Priority = (double)fee/days;
    }

    bool operator <(const Dragon& d) const
    {
        return Priority < d.Priority;
    }
};

int main()
{
    std::ios_base::sync_with_stdio(false);
    std::cin.tie(NULL);

    bool isTraining = false;
    int days, fee, remainingDays;
    double dailyFee = 0.0, totalFee = 0.0;
    std::priority_queue<Dragon> queue;

    while (std::cin >> days >> fee)
    {
        queue.push(Dragon(days, fee));
        dailyFee += fee;
        
        if (!isTraining || !remainingDays)
        {
            remainingDays = queue.top().Days;
            dailyFee -= queue.top().Fee;
            queue.pop();
            isTraining = true;
        }

        totalFee += dailyFee;
        remainingDays--;
    }

    if (isTraining)
    {
        totalFee += remainingDays * dailyFee;
    }

    while (!queue.empty()) 
    {
        dailyFee -= queue.top().Fee;
        totalFee += dailyFee * queue.top().Days;
        queue.pop();
    }

    printf("%.lf\n", totalFee);

    return 0;
}