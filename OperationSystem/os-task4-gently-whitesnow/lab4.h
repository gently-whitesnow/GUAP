//
// lab3 definitions header file
//

#ifndef LAB4_LAB4_H
#define LAB4_LAB4_H

#include <random>
#include <cassert>

// init
std::mt19937 _rng(42);    // random-number engine used (Mersenne-Twister in this case)

// std::mt19937 is persistent across platforms, but STL distributions are not
// see https://stackoverflow.com/questions/34903356/c11-random-number-distributions-are-not-consistent-across-platforms-what-al
// https://stackoverflow.com/questions/922358/consistent-pseudo-random-numbers-across-platforms
// and https://stackoverflow.com/questions/51929085/cross-platform-random-reproducibility

// the following code taken from http://stackoverflow.com/questions/44520973/how-can-i-get-an-implementation-agnostic-version-of-stduniform-int-distributio
// TODO: get rid of template class and use Mersenne-Twister generator directly in uniform_rnd function
template <class IntType = int>
struct my_uniform_int_distribution
{
    using result_type = IntType;

    const result_type A, B;

    struct param_type
    {
        const result_type A, B;

        param_type(result_type aa, result_type bb)
         : A(aa), B(bb)
        {}
    };

    explicit my_uniform_int_distribution(const result_type a = 0, const result_type b = std::numeric_limits<result_type>::max())
     : A(a), B(b)
    {}

    explicit my_uniform_int_distribution(const param_type& params)
     : A(params.A), B(params.B)
    {}

    template <class Generator>
    result_type operator()(Generator& g) const
    {
        return rnd(g, A, B);
    }

    template <class Generator>
    result_type operator()(Generator& g, const param_type& params) const
    {
        return rnd(g, params.A, params.B);
    }

    result_type a() const
    {
        return A;
    }

    result_type b() const
    {
        return B;
    }

    result_type min() const
    {
        return A;
    }

    result_type max() const
    {
        return B;
    }

private:
    template <class Generator>
    result_type rnd(Generator& g, const result_type a, const result_type b) const
    {
        static_assert(std::is_convertible<typename Generator::result_type, result_type>::value, "Ups...");
        static_assert(Generator::min() == 0, "If non-zero we have to handle the offset");
        const result_type range = b - a + 1;
        assert(Generator::max() >= range); // Just for safety
        const result_type reject_lim = g.max() % range;
        result_type n;
        do
        {
            n = g();
        }
        while (n <= reject_lim);
        return (n % range) + a;
    }
};


int uniform_rnd(int a, int b)
{
    // std::uniform_int_distribution<int> uniform_rnd(a, b); // guaranteed unbiased
    if (a == b)
        return a;
    my_uniform_int_distribution<int> uniform_rnd(a, b);
    return uniform_rnd(_rng);
}


#endif //LAB4_LAB4_H
