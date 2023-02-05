using ConsoleAppPixelDrawing;

int InputInt(string message, int minValue, int maxValue)
{
    bool isCorrectInput;
    int number = 0;
    do
    {
        try
        {
            isCorrectInput = true;

            Console.Write(message);
            number = int.Parse(Console.ReadLine());
            if (number < minValue || number > maxValue)
            {
                isCorrectInput = false;
                Console.WriteLine($"Ошибка вы ввели число за границами (от {minValue} до {maxValue})");
            }
        }
        catch
        {
            isCorrectInput = false;
            Console.WriteLine("Ошибка вы ввели не число");
        }
    } while (!isCorrectInput);

    return number;
}

Pixel[,] CreateField(int rows, int cols)
{
    return new Pixel[rows, cols];
}

Cursor CreateCursor()
{
    Cursor cursor;
    cursor.Color = (ConsoleColor)ConstColor.Yellow;
    cursor.I = 1;
    cursor.J = 1;
    cursor.Skin = 'K';
    cursor.StepSkin = (char)ConstSkin.FilledCell1;
    return cursor;
}

void FillField(Pixel[,] field)
{
    int rows = field.GetLength(0);
    int cols = field.GetLength(1);

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            field[i, j].Skin = (char)ConstSkin.EmptyCell;
            field[i, j].Color = (ConsoleColor)ConstColor.White;
        }
    }

    for (int i = 0; i < rows; i++)
    {
        field[i, 0].Skin = (char)ConstSkin.BoundCell;
        field[i, 0].Color = (ConsoleColor)ConstColor.Green;

        field[i, cols - 1].Skin = (char)ConstSkin.BoundCell;
        field[i, cols - 1].Color = (ConsoleColor)ConstColor.Green;
    }

    for (int j = 0; j < cols; j++)
    {
        field[0, j].Skin = (char)ConstSkin.BoundCell;
        field[0, j].Color = (ConsoleColor)ConstColor.Green;

        field[rows - 1, j].Skin = (char)ConstSkin.BoundCell;
        field[rows - 1, j].Color = (ConsoleColor)ConstColor.Green;
    }
}

void ShowField(Pixel[,] field, Cursor cursor)
{
    int rows = field.GetLength(0);
    int cols = field.GetLength(1);

    Console.ResetColor();
    Console.Clear();

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            if (i == cursor.I && j == cursor.J)
            {
                Console.ForegroundColor = cursor.Color;
                Console.Write(cursor.Skin);
            }
            else
            {
                Console.ForegroundColor = field[i, j].Color;
                Console.Write(field[i, j].Skin);
            }
        }

        Console.WriteLine();
    }

    Console.ResetColor();
    Console.WriteLine(@"Для управления курсором нажимайте клавиши W,A,S,D
Для смены цвета нажмите клавиши цифры 1,2,3
Для того чтобы закрасить нужным цветом клетку нажмите пробел
Для того чтобы отменить закраску клетки нажмите бекспейс");
}

void CursorProcess(Pixel[,] field, ConsoleKey key, ref Cursor cursor)
{
    int rows = field.GetLength(0);
    int cols = field.GetLength(1);

    switch (key)
    {
        case ConsoleKey.A:
            if (cursor.J > 1)
            {
                cursor.J--;
            }

            break;

        case ConsoleKey.W:
            if (cursor.I > 1)
            {
                cursor.I--;
            }

            break;

        case ConsoleKey.D:
            if (cursor.J < cols - 2)
            {
                cursor.J++;
            }

            break;

        case ConsoleKey.S:
            if (cursor.I < rows - 2)
            {
                cursor.I++;
            }

            break;

        case ConsoleKey.Spacebar:
            field[cursor.I, cursor.J].Skin = cursor.StepSkin;
            field[cursor.I, cursor.J].Color = cursor.Color;
            break;

        case ConsoleKey.Backspace:
            field[cursor.I, cursor.J].Skin = (char)ConstSkin.EmptyCell;
            field[cursor.I, cursor.J].Color = (ConsoleColor)ConstColor.White;
            break;
        
        case ConsoleKey.D1:
            cursor.Color = (ConsoleColor)ConstColor.Yellow;
            break;
        
        case ConsoleKey.D2:
            cursor.Color = (ConsoleColor)ConstColor.Red;
            break;
        
        case ConsoleKey.D3:
            cursor.Color = (ConsoleColor)ConstColor.Blue;
            break;
        
        case ConsoleKey.D4:
            cursor.StepSkin = (char)ConstSkin.FilledCell1;
            break;
        
        case ConsoleKey.D5:
            cursor.StepSkin = (char)ConstSkin.FilledCell2;
            break;
        
        case ConsoleKey.D6:
            cursor.StepSkin = (char)ConstSkin.FilledCell3;
            break;
    }
}

//------

int rows = InputInt("Введите количество строк (от 10 до 20): ", 10, 20);
int cols = InputInt("Введите количество столбцов (от 10 до 20): ", 10, 20);

Pixel[,] field = CreateField(rows, cols);
Cursor cursor = CreateCursor();

FillField(field);

while (true)
{
    ShowField(field, cursor);
    ConsoleKey key = Console.ReadKey(false).Key;
    CursorProcess(field, key, ref cursor);
}