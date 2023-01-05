#include <iostream>


using namespace std;


int sum(int c, ...)
{
    int *p = &c;
    int n = (*p); 
    p+=1;     
    int s = 0;  
    for (int i=1;i<n;i++){
        s+=(*p);         
        p+=1;        
        cout<<(*p)<<endl;   
    }     
    
    return s;  
}

int main()
{
    
    int ans = sum(4,1,2,3,4);
    cout << ans << endl;
    
}