# Vending Machine (WIP)
.Net 5 project based on clean architecture with new approach



In this project we are simulating a Vending Machine 



## Convention in naming files:

In data model, this formatting is used: **[Group name].[Class name]**, 

sample: **Order.InvoiceDetailDto**

"**Order**" is group name and it is for better reading and finding related classes more easily. <u>(this is important in big projects with lots of entities)</u>



## Innovation in data model:

​	In old school approach we create entities and then we create DTOs based on them, but there is a redundant that could lead to painful bugs.

when data model is evolving and getting bigger, we have increase in Entities and properties per entities amount and this is inevitable.

Each time with adding or removing properties in entities, developer should update DTOs accordingly and if he/she forgot, pain will present itself to the dev team.

### Now the 1 million $ question:

Why we are doing this: when we are developing a projects that is working with data objects we create entities, when we want to transfer data inside project we need to remove some properties from entity to prevent looping and... so we create DTOs based on entities, DTO is a data transfer object and it's purpose is only transferring data between project parts, that's it.

In new approach we first build DTOs and based on that we build Entities, somehow this is DRY approach as well.

### Let's see some code:

Sample DTO

```c#
public class InvoiceDetailDto : AuditableEntity, IMapFrom<InvoiceDetail>
   {
 
       public int Id { get; set; }
       public int Amount { get; set; }
       public int ItemId { get; set; }
       public int ItemPrice { get; set; }
 
       public int InvoiceId { get; set; }
 
       public void Mapping(Profile profile)
       {
           profile.CreateMap<InvoiceDetail, InvoiceDetailDto>().ReverseMap();
       }
 
   }
```

Sample Entity

```c#
public class InvoiceDetail : ItemDto
{
 
    public virtual Invoice Invoice { get; set; } 
 
}
```



We get into AuditableEntity and IMapFrom<InvoiceDetail> later

As you see we defined base properties in DTO class and the Entity Framework related in Entity class



## Layers completion progress:

### Domain layer

- [ ] Data model

​	

### Application layer



### Infrastructure layer



### Presentation layer



## Solution Details:

will be added soon



