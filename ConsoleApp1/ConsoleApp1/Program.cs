namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {

            int[] arr = new int[] { 23,4,1,24,45,13,8};

            for (int i = 0; i < arr.Length-1; i++)
            {
                for (int j = i+1; j < arr.Length; j++)
                {
                    if (arr[i] > arr[j])
                    {
                       
                        int temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
            foreach (int i in arr)
            {
                Console.WriteLine(i);
            }
        }
        
            public static int SearchInsert(int[] nums, int target)
            {

                for (int i = 0; i < nums.Length; i++)
                {
                    if (target == nums[i])
                    {
                        return i;
                    }
                }
                for (int i = 0; i < nums.Length - 1; i++)
                {
                    if (target > nums[i] && target < nums[i + 1])
                    {
                        return i + 1;
                    }
                }
                return 0;
            }
        
    }
}