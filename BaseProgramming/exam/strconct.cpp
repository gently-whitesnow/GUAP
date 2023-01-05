#include <iostream>
#include <string.h>

using namespace std;


bool isUIntNumber(const string &s)
{
    bool b = true;
    for (int i = 0; i < s.size(); i++)
    {
        if (!isdigit(s[i]))
            b =  false;
    }
    return b;
}

unsigned int getPositiveUIntValue(const string &what)
{
    string a;
    unsigned int b;
    while (true)
    {
        cin.clear();
        cout <<"Enter a positive int value of "<< what <<" array: ";
        cin >> a;
        if (isUIntNumber(a) && stoi(a) > 0)
        {
            b = stoi(a);
            break;
        }
        cout << "Bad input" << endl;
    }
    return b;
}

// Перегрузка
// Concat

// void concat(char *str1, const char *str2,unsigned int n1,unsigned int n2)
// {
//     int a = 0;
//     for (unsigned int i = (n1+n2)-n2; i < n1+n2; i++)
//     {
//         str1[i] = str2[a];
//         a++;
//     }
    
// }

char* concat(char *str1, const char *str2,unsigned int n1,unsigned int n2)
{
    char *array = new char[n1+n2+1];
    for (unsigned int i = 0; i < n1; i++)
    {
        array[i] = str1[i];
    }
    for (unsigned int i = 0; i < n2; i++)
    {
        array[n1+i] = str2[i];
    }

    array[n1+n2] = '\0';

    return array;
}

string concat(const char *str1, const char *str2, const char *str3)
{
    return str1+str2+str3;
}

char getChar(const string &what)
{
    char a;
    cin.clear();
    cout << "Enter char value of " << what <<" array"<< endl;
    cin >> a;
    
    return a;
}

void fill(char *array, unsigned int n,const string &what){

    for (unsigned int i = 0; i < n; i++)
    {
        array[i] = getChar(what);
    }
}

int length(const char* array){
    int a =0;
    while(array[a]!='\0')
    {
        a++;
    }
    return a;
}



// int main()
// {
//     unsigned int n1 = getPositiveUIntValue("first");
//     unsigned int n2 = getPositiveUIntValue("second");

//     char* a = new char[n1+n2];
//     char* b = new char[n2];

//     const char* st ="12345";
//     int f = length(st);
//     cout <<"equal: "<< f <<endl;

//     fill(a,n1,"first");
//     fill(b,n2,"second");

//     concat(a,b,n1,n2);
//     cout<<a<<endl;

//     cout << endl;
//     delete [] a;
//     delete [] b;

// }

int main()
{
    //
    const char* st ="12345";
    int f = length(st);
    cout <<"equal: "<< f <<endl;
    //
    
    char* a = "1234";
    char* b = "123456";
    cout<< "bite"<< sizeof(b)<<endl;
    cout<< "len"<< strlen(b)<<endl;
    unsigned int n1 = length(a);
    unsigned int n2 = length(b);
    
    char* ans = concat(a,b,n1,n2);
    cout<<ans<<endl;
    f = length(ans);
    cout <<"equal: "<< f <<endl;

    delete [] ans;
}



