/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [Item]
      ,[Column1]
      ,[LOT#]
      ,[PLS%]
      ,[Variety]
      ,[Purity]
      ,[Crop]
      ,[Inert]
      ,[Weeds]
      ,[Noxious Weeds]
      ,[Germ]
      ,[Dormant]
      ,[Total]
      ,[Test Date]
      ,[Orgin]
      ,[Distributor]
      ,[Wholesale]
      ,[Retail]
      ,[Item 1]
      ,[Item 2]
      ,[QTY]
      ,[Reorder QTY]
      ,[REORDER]
      ,[Discontinued]
      ,[Column 24]
      ,[Column 25]
      ,[Column 26]
  FROM [Seed].[dbo].[ItemsSource]


  select cast(item as nvarchar(100)) as Item, cast(Lot# as nvarchar(100)) as Lot,
 cast(replace([PLS%],'%','') as decimal(12,4))/100.00  as [PLS%],
  cast(variety as nvarchar(100)) as variety,
  cast(replace([Purity],'%','') as decimal(12,4))/100.00  as [Purity],
  cast(replace([Crop],'%','') as decimal(12,4))/100.00  as [Crop],
  cast(replace([Weeds],'%','') as decimal(12,4))/100.00  as [Weeds],
  cast(replace([Germ],'%','') as decimal(12,4))/100.00  as [Germ],
  cast(replace([Dormant],'%','') as decimal(12,4))/100.00  as [Dormant],
  cast(replace([Total],'%','') as decimal(12,4))/100.00  as [Total],
  [Test Date],
  cast(orgin as nvarchar(100)) as Orgin,
  cast(replace([Distributor],'$','') as decimal(12,4))  as [Distributor],
  cast(replace([Wholesale],'$','') as decimal(12,4))  as [Wholesale],
  cast(replace(Retail,'$','') as decimal(12,4))  as Retail,
  cast([Item 1] as nvarchar(100)) as Item1,
  cast([Item 2] as nvarchar(100)) as Item2,
  --cast([qty] as decimal(12,4)) as QTY,
  cast(0 as decimal(12,4)) as ReorderQTY,
  cast(case REORDEr when '1.00' then 1 when '0.00' then 0 else 0 end as bit) as Reorder,
  Cast(0 as bit) Discontinued,
  [column 26] as ScientificName into Seed.dbo.Items
  from seed.dbo.ItemsSource
  
  select * from seed.dbo.ItemsSource order by [Reorder QTY]

--  delete from seed.dbo.ItemsSource where [pls%]=' '

update seed.dbo.ItemsSource set Weeds=0.00 where Weeds=''
update seed.dbo.ItemsSource set crop=0.00 where crop=''
update seed.dbo.ItemsSource set Purity=0.00 where Purity=''
update seed.dbo.ItemsSource set Germ=0.00 where Germ=''
update seed.dbo.ItemsSource set Dormant=0.00 where Dormant=''
update seed.dbo.ItemsSource set Total=0.00 where Total=''
update seed.dbo.ItemsSource set Distributor=0.00 where Distributor=''
update seed.dbo.ItemsSource set Distributor=1.00 where Distributor='"$1'
update seed.dbo.ItemsSource set Wholesale=0.00 where Wholesale=''
update seed.dbo.ItemsSource set Wholesale=1.00 where Wholesale='"$1'
update seed.dbo.ItemsSource set Wholesale=0.00 where Wholesale='000.00"'
update seed.dbo.ItemsSource set Wholesale=200.00 where Wholesale='200.00"'
update seed.dbo.ItemsSource set Retail=0.00 where Retail=''
update seed.dbo.ItemsSource set Retail=1.00 where Retail='"$1'
update seed.dbo.ItemsSource set Retail=1.00 where Retail='"$1'
update seed.dbo.ItemsSource set QTY=0.00 where QTY=''