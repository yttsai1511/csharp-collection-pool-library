# C# Collection Pool Library

一個簡單、彈性且高效的 C# 集合物件池，適用於需要頻繁分配和釋放集合的場景，有效降低記憶體分配次數，提升程式效能。

## 功能特色

- **集合支援**：內建對多種 .NET 集合類型的物件池管理：
    - `List<T>`
    - `HashSet<T>`
    - `Queue<T>`
    - `Stack<T>`
    - `Dictionary<TKey, TValue>`
- **線程安全**：多線程環境下操作安全可靠，避免資源競爭。
- **效能優化**： 透過物件池重複使用集合物件，有效降低 GC 壓力。
- **彈性設定**：提供最大池容量的自訂設定，能根據需求調整。

## 使用範例

以下範例展示如何使用物件池快速管理集合物件的分配與釋放：

```csharp
var groups = DictionaryPool<int, List<int>>.Get(); // 從池中取得 Dictionary

for (int i = 0; i < 10; i++)
{
    var items = ListPool<int>.Get(); // 從池中取得 List

    // 新增資料到列表
    // items.Add(...);

    groups.Add(i + 100, items);
}

// 進行字典的操作或整理
// ...

foreach (var kvp in groups)
{
    ListPool<int>.Release(kvp.Value); // 清空並釋放 List 回池中
}

DictionaryPool<int, List<int>>.Release(groups); // 清空並釋放 Dictionary 回池中
```

## 文件說明
| 方法名稱 | 功能描述 |
| :----- | :----- |
| `Get` | 從物件池中取得集合，若池中無可用對象則自動分配新對象 |
| `Release` | 將集合清空後釋放回物件池，供未來重複使用 |
| `Clear` | 清空物件池中的所有對象 |
| `MaxPoolSize` | 自訂或取得物件池的最大容量，預設值為 255 |

## 授權

此專案基於 GPLv3 授權條款，詳情請參閱 LICENSE 文件。