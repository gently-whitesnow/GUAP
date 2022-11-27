using System;
using System.Collections.Generic;
using System.Linq;

namespace FileDatabase;

public class BdContext
{

    private int _id=1;
    private List<Train> staticData;
    
    public BdContext()
    {
        staticData = Initialise();
        if(staticData.Count==0)
            FillMockValues();
    }
    public List<Train> Initialise()
    {
        return new List<Train>();
    }
    
    public List<Train> Select(Func<IEnumerable<Train>,IEnumerable<Train>> filter, Func<IEnumerable<Train>,IEnumerable<Train>> order)
    {
        return order(filter(staticData)).ToList();
    }
    
    public void Insert(Train train)
    {
        train.Id = _id++;
        staticData.Add(train);
    }
    
    public void Update()
    {
        
    }
    
    public void Delete()
    {
        
    }
    
    public void Save()
    {
        
    }

    public void FillMockValues()
    {
        Insert(new Train("N322","Moscow",DateTime.ParseExact("14.12.1999","dd.MM.yyyy", null)));
        Insert(new Train("N321","Peter",DateTime.ParseExact("14.12.2000","dd.MM.yyyy", null)));
        Insert(new Train("N323","Kaluga",DateTime.ParseExact("15.12.1999","dd.MM.yyyy", null)));
        Insert(new Train("N324","Tula",DateTime.ParseExact("14.11.1999","dd.MM.yyyy", null)));
        Insert(new Train("N325","Koselsk",DateTime.ParseExact("14.12.1998","dd.MM.yyyy", null)));
        Insert(new Train("N326","Krim",DateTime.ParseExact("21.12.2000","dd.MM.yyyy", null)));
        Insert(new Train("N322","Moscow",DateTime.ParseExact("14.12.1999","dd.MM.yyyy", null)));
        Insert(new Train("N321","Peter",DateTime.ParseExact("14.12.2000","dd.MM.yyyy", null)));
        Insert(new Train("N323","Kaluga",DateTime.ParseExact("15.12.1999","dd.MM.yyyy", null)));
        Insert(new Train("N324","Tula",DateTime.ParseExact("14.11.1999","dd.MM.yyyy", null)));
        Insert(new Train("N325","Koselsk",DateTime.ParseExact("14.12.1998","dd.MM.yyyy", null)));
        Insert(new Train("N326","Krim",DateTime.ParseExact("21.12.2000","dd.MM.yyyy", null)));
        Insert(new Train("N322","Moscow",DateTime.ParseExact("14.12.1999","dd.MM.yyyy", null)));
        Insert(new Train("N321","Peter",DateTime.ParseExact("14.12.2000","dd.MM.yyyy", null)));
        Insert(new Train("N323","Kaluga",DateTime.ParseExact("15.12.1999","dd.MM.yyyy", null)));
        Insert(new Train("N324","Tula",DateTime.ParseExact("14.11.1999","dd.MM.yyyy", null)));
        Insert(new Train("N325","Koselsk",DateTime.ParseExact("14.12.1998","dd.MM.yyyy", null)));
        Insert(new Train("N326","Krim",DateTime.ParseExact("21.12.2000","dd.MM.yyyy", null)));
    }
    
}