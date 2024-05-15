Design Features Used:
1. Impemented Clean Architecture separating exposing API layer, Business layer for Core Business Logic, Domain layer for Models and Infrastructure layer for external dependencies interaction.
2. Separate Rest API and associated Business Service based on Resources for separate functionalities i.e. Cart, Discount, User promoting modular monolith architecture for easy microservice segregation based on future needs.
3. Strategy Pattern to support different discount Strategies
4. Factory Pattern to support different discount strategy object creation.
5. Repository pattern to create uniform interface and decouple and restrain access to the dbcontext object.
6. Singleton used to create a inmemory caching service and reduce discount objects creation overhead to be used accross application lifetime
7. Builder Pattern used to build .net Core application app pipeline in Program.cs
8. JWT Authentication - Token generation on login and token validation from Auth header on requests.
9. Roles Based Authorization and Access Control - User Role for cart functionalities, Admin role for Discount Addition and Expiry.
10. Custom middleware to parse Auth token, and store required claims in context
11. Custom Attribute to implement role check on different endpoints
12. Automapper for mapping requirements.
13. Sql Server as db and EF Core for ORM.
14. Swagger for API Testing during development.
15. SOLID used
S - Single Responsibility - Each class restricted to its specific core implementation to make sure minimal change.
O - Open Close Principle - Interface based Strategies and json based discount properties used to allow extension based on new discount features coming in furture making sure existing classes are not modified unless required.
L - Liskovs substitution princple - Interface based implementation used to make sure LSP principle is not breached and is used extensively in Dependency injection and feature implement.
I - Interface Segregation - Interfaces segregated based on different functionalities so no class needs to implement functionalities it does not needs.
D - Dependency Inversion - Constructor based Dependency Injection used to inplement Dependency Inversion abstracting high level objects from low level objects.
16. Nunit used for unit testing and MOQ used for dependency mocking.
17. Angular UI implemented to interact with WebAPIs. 
	a. SubComponent compartmentalization used to ensure reusability of components such as productdetail component as a child component for product list passing each product as input to child component.
	b. Feature developed 
		- /Product - Page interacting with products and webapi, fetching products, parsing response and displaying products with details to enable customers to add to cart and quantity.
		- /cart - Page interacting with cart api to fetch list of items added in cart, show the unit proice, disocunt, discount code used and special message if any along with summary of entire cart with totalamount, totaldiscount and net amount.
		- Cart Service - service injected in root added to enable webapi interaction from different components.
		- Router - Router based application navigation added injected in root to support product and cart navigation.
		- Flex based wrapping added to support minimal responsiveness.(Bootstrap based complete responsiveness pending implementation)

