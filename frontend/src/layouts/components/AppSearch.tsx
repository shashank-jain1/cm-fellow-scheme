import { useState, useRef, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { searchIndex } from '../data/searchIndex';

export default function AppSearch() {
  const navigate = useNavigate();
  const [globalSearch, setGlobalSearch] = useState('');
  const [searchOpen, setSearchOpen] = useState(false);
  const searchInputRef = useRef<HTMLInputElement>(null);
  const searchContainerRef = useRef<HTMLDivElement>(null);

  const filteredSearch = globalSearch.trim()
    ? searchIndex.filter(
        (item) =>
          item.title.toLowerCase().includes(globalSearch.toLowerCase()) ||
          item.category.toLowerCase().includes(globalSearch.toLowerCase())
      )
    : [];

  useEffect(() => {
    function handleKeyDown(e: KeyboardEvent) {
      if ((e.ctrlKey || e.metaKey) && e.key === 'k') {
        e.preventDefault();
        searchInputRef.current?.focus();
        setSearchOpen(true);
      }
      if (e.key === 'Escape') {
        setSearchOpen(false);
      }
    }

    function handleClickOutside(e: MouseEvent) {
      if (searchContainerRef.current && !searchContainerRef.current.contains(e.target as Node)) {
        setSearchOpen(false);
      }
    }

    document.addEventListener('keydown', handleKeyDown);
    document.addEventListener('mousedown', handleClickOutside);
    return () => {
      document.removeEventListener('keydown', handleKeyDown);
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, []);

  const handleSelect = (path: string) => {
    navigate(path);
    setGlobalSearch('');
    setSearchOpen(false);
  };

  return (
    <div ref={searchContainerRef} className="header-search-bar">
      <i className="pi pi-search header-search-icon" />
      <input
        ref={searchInputRef}
        type="text"
        placeholder="Search modules, fellows, activities..."
        value={globalSearch}
        onFocus={() => setSearchOpen(true)}
        onChange={(e) => {
          setGlobalSearch(e.target.value);
          setSearchOpen(true);
        }}
        className="header-search-input"
      />
      <kbd className="header-search-kbd">Ctrl K</kbd>

      {searchOpen && filteredSearch.length > 0 && (
        <div className="header-search-results">
          {filteredSearch.map((item) => (
            <div
              key={item.path}
              className="search-result-item"
              onClick={() => handleSelect(item.path)}
            >
              <i className={item.icon} style={{ fontSize: 14, color: 'var(--accent)' }} />
              <span style={{ fontWeight: 600 }}>{item.title}</span>
              <span className="search-result-category">{item.category}</span>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
