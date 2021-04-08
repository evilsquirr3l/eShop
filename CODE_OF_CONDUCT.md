# Contributor Covenant Code of Conduct

## Требования к именам коммитов
- Названия коммитов должны быть согласно [гайдлайну](https://www.conventionalcommits.org/en/v1.0.0/)
- Должен использоваться present tense ("add feature" not "added feature")
- Должен использоваться imperative mood ("move cursor to..." not "moves cursor to...")

### Примеры имен коммитов
- `init:` - используется для начала проекта / таски. Примеры:
```
init: start lab6
init: start eshop project
```
- `feat:` - это реализованная новая функциональность из технического задания (добавил метод добавления пользователя, добавил footer, добавил карточку продукта). Примеры:
```
feat: add basic page layout
feat: implement search box 
feat: implement request to Privatbank API
feat: add additional navigation button
feat: add banner
```
- `fix:` - исправил ошибку в ранее реализованной функциональности. Примеры:
```
fix: implement correct loading data from PostgreSQL
fix: make entity AsNoTracking to avoid exception in Update
fix: adjust navbar for mobile
```
- `refactor:` - новой функциональности не добавлял / поведения не менял. Файлы в другие места положил, удалил, добавил. Изменил форматирование кода (white-space, formatting, missing semi-colons, etc). Улучшил алгоритм, без изменения функциональности. Примеры:
```
refactor: change structure of the project
refactor: rename vars for better readability
refactor: apply eslint
refactor: apply prettier
refactor: apply sonarqube rules
```
- `docs:` - используется при работе с документацией/readme проекта. Примеры:
```
docs: update readme with additional information
docs: update description of run() method
```