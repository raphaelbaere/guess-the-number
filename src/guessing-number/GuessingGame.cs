using System;

namespace guessing_number;

public class GuessNumber
{
    //In this way we are passing the random number generator by dependency injection
    private IRandomGenerator random;
    public GuessNumber() : this(new DefaultRandom()) { }
    public GuessNumber(IRandomGenerator obj)
    {
        this.random = obj;

        userValue = 0;
        randomValue = 0;
    }

    //user variables
    public int userValue;
    public int randomValue;

    public int maxAttempts = 5;
    public int currentAttempts = 0;

    public int difficultyLevel = 1;

    public bool gameOver = false;

    //1 - Imprima uma mensagem de saudação
    public string Greet()
    {
        string greet = "---Bem-vindo ao Guessing Game--- /n Para começar, tente adivinhar o número que eu pensei, entre -100 e 100!";
        return greet;
    }

    //2 - Receba a entrada da pessoa usuária e converta para Int
    //5 - Adicione um limite de tentativas
    public string ChooseNumber(string userEntry)
    {  
        currentAttempts += 1;
        if (currentAttempts > maxAttempts)
        {
            string errorMessage = "Você excedeu o número máximo de tentativas! Tente novamente.";
            gameOver = true;
            return errorMessage;
        }
        bool isNumber = Int32.TryParse(userEntry, out userValue);
        if (!isNumber)
        {
            string errorMessage = "Entrada inválida! Não é um número.";
            userValue = 0;
            return errorMessage;
        } else if (!(userValue >= -100 && userValue <= 100))
        {
            string errorMessage = "Entrada inválida! Valor não está no range.";
            userValue = 0;
            return errorMessage;
        }
        string successMessage = "Número escolhido!";
        return successMessage;
    }

    //3 - Gere um número aleatório
    public string RandomNumber()
    {
        randomValue = random.GetInt(-100, 100);
        string returnMessage = "A máquina escolheu um número de -100 à 100!";
        return returnMessage;
    }

    //6 - Adicione níveis de dificuldade
    public string RandomNumberWithDifficult()
    {
        string message = "";
        if (difficultyLevel == 2)
        {
           randomValue = random.GetInt(-500, 500);
           message = "A máquina escolheu um número de -500 à 500!";
           return message;
        } else if (difficultyLevel == 3)
        {
           randomValue = random.GetInt(-1000, 1000);
           message = "A máquina escolheu um número de -1000 à 1000!";
           return message;
        }
        randomValue = random.GetInt(-100, 100);
        message = "A máquina escolheu um número de -100 à 100!";
        return message;
    }

    //4 - Verifique a resposta da jogada
    public string AnalyzePlay()
    {
        string message = "";
        if (userValue > randomValue)
        {
            message = "Tente um número MENOR";
            return message;
        } else if (userValue < randomValue)
        {
            message = "Tente um número MAIOR";
            return message;
        }
        message = "ACERTOU!";
        gameOver = true;
        return message;
    }

    //7 - Adicione uma opção para reiniciar o jogo
    public void RestartGame()
    {
        currentAttempts = 0;
        userValue = 0;
        randomValue = 0;
        difficultyLevel = 1;
        maxAttempts = 5;
        gameOver = false;
    }
}