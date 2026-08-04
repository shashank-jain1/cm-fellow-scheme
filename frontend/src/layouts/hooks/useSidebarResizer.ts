import { useCallback, useEffect, useState } from 'react';

interface SidebarResizerProps {
  onWidthChange?: (width: number) => void;
  collapsed: boolean;
}

export function useSidebarResizer(onWidthChange?: (width: number) => void) {
  const [isResizing, setIsResizing] = useState(false);

  const handleMouseDown = useCallback((e: React.MouseEvent) => {
    e.preventDefault();
    setIsResizing(true);
  }, []);

  useEffect(() => {
    if (!isResizing || !onWidthChange) return;
    const handleMouseMove = (e: MouseEvent) => {
      const newWidth = Math.min(Math.max(e.clientX, 210), 320);
      onWidthChange(newWidth);
    };
    const handleMouseUp = () => setIsResizing(false);
    document.addEventListener('mousemove', handleMouseMove);
    document.addEventListener('mouseup', handleMouseUp);
    document.body.style.userSelect = 'none';
    document.body.style.cursor = 'col-resize';
    return () => {
      document.removeEventListener('mousemove', handleMouseMove);
      document.removeEventListener('mouseup', handleMouseUp);
      document.body.style.userSelect = '';
      document.body.style.cursor = '';
    };
  }, [isResizing, onWidthChange]);

  return { isResizing, handleMouseDown };
}

export default function SidebarResizer({ onWidthChange, collapsed }: SidebarResizerProps) {
  const { isResizing, handleMouseDown } = useSidebarResizer(onWidthChange);
  const transition = isResizing ? 'none' : 'width var(--transition-slow)';

  return { isResizing, handleMouseDown, transition };
}
