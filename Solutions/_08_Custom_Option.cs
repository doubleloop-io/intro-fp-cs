using Xunit;

namespace IntroFp.Solutions;

public class _08_Custom_Option
{
    private static int Increment(int x) => x + 1;

    private static IOption<string> ReverseString(int x) => Prelude.Some(new string(x.ToString().Reverse().ToArray()));

    private interface IOption<T>
    {
        public IOption<TResult> Map<TResult>(Func<T, TResult> func);
        public IOption<TResult> Bind<TResult>(Func<T, IOption<TResult>> func);
        public TResult Match<TResult>(Func<T, TResult> onSome, Func<TResult> onNone);
    }

    private record Some<T>(T Value) : IOption<T>
    {
        public IOption<TResult> Map<TResult>(Func<T, TResult> func) =>
            new Some<TResult>(func(Value));

        public IOption<TResult> Bind<TResult>(Func<T, IOption<TResult>> func) =>
            func(Value);

        public TResult Match<TResult>(Func<T, TResult> onSome, Func<TResult> onNone) =>
            onSome(Value);
    }

    private record None<T> : IOption<T>
    {
        public IOption<TResult> Map<TResult>(Func<T, TResult> func) =>
            new None<TResult>();

        public IOption<TResult> Bind<TResult>(Func<T, IOption<TResult>> func) =>
            new None<TResult>();

        public TResult Match<TResult>(Func<T, TResult> onSome, Func<TResult> onNone) =>
            onNone();
    }

    private static class Prelude
    {
        public static IOption<T> Some<T>(T value) => new Some<T>(value);
        public static IOption<T> None<T>() => new None<T>();
    }

    [Fact(Skip = "TODO")]
    public void creation_phase()
    {
        var result = Prelude.Some(10);

        Assert.Equal(new Some<int>(10), result);
    }

    [Fact(Skip = "TODO")]
    public void combination_phase_normal()
    {
        var result = Prelude.Some(10)
            .Map(Increment);

        Assert.Equal(new Some<int>(11), result);
    }

    [Fact(Skip = "TODO")]
    public void combination_phase_effectful()
    {
        var result = Prelude.Some(10)
            .Bind(ReverseString);

        Assert.Equal(new Some<string>("01"), result);
    }

    [Fact(Skip = "TODO")]
    public void removal_phase_value()
    {
        var result = Prelude.Some(10)
            .Match(x => x.ToString(), () => "none");

        Assert.Equal("10", result);
    }

    [Fact(Skip = "TODO")]
    public void removal_phase_alternative_value()
    {
        var result = Prelude.None<int>()
            .Match(x => x.ToString(), () => "none");

        Assert.Equal("none", result);
    }
}
