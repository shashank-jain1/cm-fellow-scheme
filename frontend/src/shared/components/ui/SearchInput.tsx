import { InputText } from 'primereact/inputtext';

interface SearchInputProps {
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
  style?: React.CSSProperties;
}

export default function SearchInput({ value, onChange, placeholder = 'Search...', style }: SearchInputProps) {
  return (
    <div className="search-input-wrapper" style={style}>
      <i className="pi pi-search" />
      <InputText
        value={value}
        onChange={(e) => onChange(e.target.value)}
        placeholder={placeholder}
        style={{ width: '100%' }}
      />
    </div>
  );
}
