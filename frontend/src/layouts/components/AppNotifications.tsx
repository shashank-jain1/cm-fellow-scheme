import { useState, useRef, useEffect } from 'react';

const mockNotifications = [
  { id: 1, title: 'New Leave Request', time: '10m ago', unread: true, icon: 'pi pi-calendar-plus', color: 'var(--accent)' },
  { id: 2, title: 'Activity Scheduled', time: '1h ago', unread: true, icon: 'pi pi-calendar', color: 'var(--info)' },
  { id: 3, title: 'Certificate Approved', time: '3h ago', unread: false, icon: 'pi pi-check-circle', color: 'var(--success)' },
];

export default function AppNotifications() {
  const [showNotifications, setShowNotifications] = useState(false);
  const notifRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    function handleClickOutside(e: MouseEvent) {
      if (notifRef.current && !notifRef.current.contains(e.target as Node)) {
        setShowNotifications(false);
      }
    }
    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);

  return (
    <div ref={notifRef} style={{ position: 'relative' }}>
      <button
        className="header-icon-btn"
        title="Notifications"
        onClick={() => setShowNotifications(!showNotifications)}
      >
        <i className="pi pi-bell" />
        <span className="notification-dot" />
      </button>

      {showNotifications && (
        <div className="header-dropdown-menu notif-dropdown">
          <div className="dropdown-header">
            <span style={{ fontWeight: 700, fontSize: 14 }}>Notifications</span>
            <span className="badge badge-info">2 New</span>
          </div>
          <div className="notif-list">
            {mockNotifications.map((n) => (
              <div key={n.id} className={`notif-item ${n.unread ? 'unread' : ''}`}>
                <div className="notif-icon-box" style={{ background: 'var(--accent-light)', color: n.color }}>
                  <i className={n.icon} />
                </div>
                <div style={{ flex: 1 }}>
                  <div style={{ fontSize: 13, fontWeight: n.unread ? 600 : 400, color: 'var(--text-heading)' }}>
                    {n.title}
                  </div>
                  <div style={{ fontSize: 11, color: 'var(--text-muted)', marginTop: 2 }}>{n.time}</div>
                </div>
              </div>
            ))}
          </div>
          <div className="dropdown-footer">
            <button className="btn-link" onClick={() => setShowNotifications(false)}>View All Notifications</button>
          </div>
        </div>
      )}
    </div>
  );
}
