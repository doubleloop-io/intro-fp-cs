using Xunit;

namespace IntroFp;

// TODO 1: for each test, remove the skip marker and make it green
public class _08_Custom_Option
{
    // just dump functions
    private static int Increment(int x) => x + 1;
    private static IOption<string> ReverseString(int x) => Prelude.Some(new string(x.ToString().Reverse().ToArray()));

    [Fact(Skip = "TODO")]
    public void creation_phase()
    {
        // TODO 2: implement 'Prelude.Some' function
        var result = Prelude.Some(10);

        Assert.Equal(new Some<int>(10), result);
    }

    [Fact(Skip = "TODO")]
    public void combination_phase_normal()
    {
        var result = Prelude.Some(10)
            // TODO 3: implement 'Map' function for Some and None
            .Map(Increment);

        Assert.Equal(new Some<int>(11), result);
    }

    [Fact(Skip = "TODO")]
    public void combination_phase_effectful()
    {
        var result = Prelude.Some(10)
            // TODO 4: implement 'Bind' function for Some and None
            .Bind(ReverseString);

        Assert.Equal(new Some<string>("01"), result);
    }

    [Fact(Skip = "TODO")]
    public void removal_phase_value()
    {
        var result = Prelude.Some(10)
            // TODO 5: implement 'Match' function for Some and None
            .Match(x => x.ToString(), () => "none");

        Assert.Equal("10", result);
    }

    [Fact(Skip = "TODO")]
    public void removal_phase_alternative_value()
    {
        // TODO 6: implement 'Prelude.None' function
        var result = Prelude.None<int>()
            .Match(x => x.ToString(), () => "none");

        Assert.Equal("none", result);
    }

    private interface IOption<T>
    {
        public IOption<TResult> Map<TResult>(Func<T, TResult> func);
        public IOption<TResult> Bind<TResult>(Func<T, IOption<TResult>> func);
        public TResult Match<TResult>(Func<T, TResult> onSome, Func<TResult> onNone);
    }

    private record Some<T>(T Value) : IOption<T>
    {
        public IOption<TResult> Map<TResult>(Func<T, TResult> func) =>
            throw new NotImplementedException();

        public IOption<TResult> Bind<TResult>(Func<T, IOption<TResult>> func) =>
            throw new NotImplementedException();

        public TResult Match<TResult>(Func<T, TResult> onSome, Func<TResult> onNone) =>
            throw new NotImplementedException();
    }

    private record None<T> : IOption<T>
    {
        public IOption<TResult> Map<TResult>(Func<T, TResult> func) =>
            throw new NotImplementedException();

        public IOption<TResult> Bind<TResult>(Func<T, IOption<TResult>> func) =>
            throw new NotImplementedException();

        public TResult Match<TResult>(Func<T, TResult> onSome, Func<TResult> onNone) =>
            throw new NotImplementedException();
    }

    private static class Prelude
    {
        public static IOption<T> Some<T>(T value) =>
            throw new NotImplementedException();

        public static IOption<T> None<T>() =>
            throw new NotImplementedException();
    }
}
