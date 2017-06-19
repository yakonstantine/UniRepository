# UniRepository
*universal repository for .net*

UniRepository is a generic repository pattern implementation for .Net (with async version), based on .Net Satndard. It's goal is to create REST repository for any entity in one click. 

Now implemented support of Entity Framework 6, in the future will be implemented support of Entity Framework 7 and, after that, will be created the solution independent of ORM version. 

## Example of usage
### Creation of RepositoryManager
```c#
// You should create your implementation of the RepositoryManager of the inherited BaseUniRepositoryManager
public class MyRepositoryManager : BaseUniRepositoryManager
    {
        // This method should return a new version of your DbContext
        // It is necessary for using in async repositories 
        protected override DbContext GetDBContext()
        {
            return new DbContext();
        }
    }
```
    
### Creation of Entity
```c#
// You should inherit all your entities from IEntity<TKey>
public class MyEntity : IEntity<int>
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        // You should implement this method for copy values of all properties to this
        // from another instance
        public void UpdateFrom(IEntity<int> entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Input argument is null.");

            if (this.GetType() != entity.GetType())
                throw new ArgumentException($"Input argument should be type of {this.GetType().Name}");

            this.Id = entity.Id;
            this.Name = ((MyEntity)entity).Name;
        }
    }
```

### Usage
```c#
 /// If you want to update related collection (add or remove one),
 /// you should call the Include method when saving the updated entity,
 /// otherwise you'll add all related objects from the collection as new.
 public async Task Async_UpdateEntityWith_OneToMany_SaveWithInclude()
     {
          var repositoryManager = new MyRepositoryManager();
          var myEntityReposotory = repositoryManager.GetRepositoryAsyncFor<MyEntity, int>();
          
          var tag1Name = "Tag1Name";
          var tag2Name = "Tag2Name";
          
          // Create new entity with one related object
          var myEntity = new MyEntity();
          user.Tags.Add(CreateNewTag(tag1Name));
          
          // For insertig entity with related obj into db
          // You need to call the SaveAsync(TEntity entity) method
          await myEntityReposotory.SaveAsync(myEntity);
          
          // Find entity by Id
          var entityFromDbWithIncludedRelatedObj = await myEntityReposotory
          // For include related object of collection you should to call the Include method
                .Include(x => x.Tags)
                .FindByKeyAsync(user.Id);
                
          // Add new tag to from from db
          entityFromDbWithIncludedRelatedObj.Tags.Add(CreateNewTag(tag2Name));
          
          // You should to call the Include method on related objects before saving
          // otherwise you'll add all related objects from the collection as new.
          await userReposotory
                .Include(x => x.Tags)
                .SaveAsync(userFromDbWithIncludedTag);
        }
 ```

    
